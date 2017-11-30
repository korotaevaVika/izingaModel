using System;

namespace Model_Izinga_WPF.Model
{
    public class ModelIzinga
    {

        const double MUb = 9.274009987354746E-24;
        const double k = 1.0E-3;
        const double g = -87941001136.19543;
        const int seed = 42;

        double T, J, B;
        int N;
        bool[,] net;
        Random rnd = new Random(seed);

        public ModelIzinga(int dim0, int dim1)
        {
            GenerateInitialGrid(dim0, dim1);
        }

        public Tuple<bool[,], double> Run(double T = 1.0, double J = 1.0, double B = 0.0)
        {
            this.T = T;
            this.J = J;
            this.B = B;

            double EnergyPrev = CalculateE(net);
            //Console.WriteLine(EnergyPrev);

            int dim0 = net.GetLength(0);
            int dim1 = net.GetLength(1);
            for (int i = 0; i < 30 * N; i++)
            {
                Tuple<int, int> currSpin = ChooseRandom(dim0, dim1);
                SwapGrid(currSpin);
                double EnergyCurr = CalculateE(net);

                if (EnergyCurr > EnergyPrev && TurnBack(EnergyCurr, EnergyPrev))
                    SwapGrid(currSpin);
                EnergyPrev = EnergyCurr;

                //Console.WriteLine(EnergyCurr);
                //PrintGrid();
            }

            //Console.WriteLine(EnergyPrev);
            return new Tuple<bool[,], double>(net, EnergyPrev);
        }


        void GenerateInitialGrid(int dim0, int dim1)
        {
            net = new bool[dim0, dim1];
            N = dim0 * dim1;

            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                    net[i, j] = rnd.NextDouble() > 0.5;
            }
        }

        Tuple<int, int> ChooseRandom(int dim0, int dim1)
        {
            return new Tuple<int, int>(rnd.Next(dim0), rnd.Next(dim1));
        }

        double EvalSpin(bool spin) => spin ? 1.0 : -1.0;

        double CalculateE(bool[,] grid)
        {
            double sum = 0.0;
            for (int i = 0; i < grid.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < grid.GetLength(1) - 1; j++)
                {
                    double sij = EvalSpin(grid[i, j]);
                    double si1j = EvalSpin(grid[i + 1, j]);
                    double sij1 = EvalSpin(grid[i, j + 1]);
                    sum += -J * (sij * si1j + sij * sij1) - B * g * MUb * sij;
                }
            }
            return sum / (double)N;
        }

        void SwapGrid(Tuple<int, int> spin)
        {
            net[spin.Item1, spin.Item2] = !net[spin.Item1, spin.Item2];
        }

        bool TurnBack(double Enew, double Eprev)
        {
            double R = Math.Exp(-(Enew - Eprev) / (k * T));
            bool ans = R < rnd.NextDouble();
            Console.WriteLine(ans);
            return ans;
        }

        Tuple<double, double> CalculateAverageEs(bool[,] net)
        {
            double EnergyPrev = CalculateE(net);

            int dim0 = net.GetLength(0);
            int dim1 = net.GetLength(1);

            int M = 100;
            double sumE = 0.0;
            double sumESquare = 0.0;
            for (int i = 0; i < M; i++)
            {
                Tuple<int, int> currSpin = ChooseRandom(dim0, dim1);
                SwapGrid(currSpin);
                double EnergyCurr = CalculateE(net);

                if (EnergyCurr > EnergyPrev && TurnBack(EnergyCurr, EnergyPrev))
                    SwapGrid(currSpin);
                EnergyPrev = EnergyCurr;
                sumE += EnergyCurr;
                sumESquare += EnergyCurr * EnergyCurr;
            }

            sumE /= (double)M;
            sumESquare /= (double)M;

            //first arg is mean of Es, second - E**2s
            return new Tuple<double, double>(sumE, sumESquare);
        }

        //returns U, C, M corresp. Launch only after Run()
        public Tuple<double, double, double> CalculateDynamicValues(double T)
        {
            Tuple<double, double> Es = CalculateAverageEs(this.net);
            double Eaver = Es.Item1;
            double Esqaver = Es.Item2;
            double U = Eaver;
            double C = (Esqaver - Eaver * Eaver) / (k * T * T * N);
            double M = 0.0;
            foreach (bool sij in net)
            {
                M += EvalSpin(sij);
            }
            M /= (double)N;

            return new Tuple<double, double, double>(U, C, M);
        }

    }
}