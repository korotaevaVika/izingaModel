using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using Model_Izinga_WPF.Model;
using System.Windows.Input;
using Model_Izinga_WPF.Command;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace Model_Izinga_WPF.ViewModel
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
            this.OnPropertyChanged(propertyName);
        }
    }
    public class ViewModel : BindableBase
    {
        public class GraphicModel
        {
            public GraphicModel(string title, string y_axis_name, string y_axis_unit)
            {
                this.y_axis_name = y_axis_name;
                this.y_axis_unit = y_axis_unit;

                Points = new List<Point>();

                PlotModel = new PlotModel();
                PlotModel.Title = title;
                FirstSeries = new LineSeries();
                X = new LinearAxis();
                Y = new LinearAxis();
            }

            public void Clear()
            {
                FirstSeries.Points.Clear();
                PlotModel.Axes.Clear();
                PlotModel.Series.Clear();
            }

            public void Update()
            {
                Clear();

                X = new LinearAxis()
                {
                    Position = AxisPosition.Bottom,
                    Minimum = Points.Count != 0 ? (Points.Min(x => x.X) - 1) : 0,
                    Maximum = Points.Count != 0 ? (Points.Max(x => x.X) + 1) : 0,
                    IsZoomEnabled = true,
                    Title = "T",
                    Unit = null,
                    TickStyle = TickStyle.Outside
                };
                Y = new LinearAxis()
                {
                    Position = AxisPosition.Left,
                    IsPanEnabled = false,
                    Minimum = Points.Count != 0 ? (Points.Min(x => x.Y) - 1) : 0,
                    Maximum = Points.Count != 0 ? (Points.Max(x => x.Y) + 1) : 0,
                    IsZoomEnabled = true,

                    Title = y_axis_name,
                    Unit = y_axis_unit,
                    TickStyle = TickStyle.Outside
                };

                Points.ForEach(x =>
                {
                    FirstSeries.Points.Add(new DataPoint(x.X, x.Y));
                });
                PlotModel.Series.Add(FirstSeries);

                PlotModel.Axes.Add(X);
                PlotModel.Axes.Add(Y);

                PlotModel.InvalidatePlot(true);
            }
            public LineSeries FirstSeries;
            public PlotModel PlotModel { get; set; }
            LinearAxis X { get; set; }
            LinearAxis Y { get; set; }
            private string y_axis_name;
            private string y_axis_unit;
            
            public List<Point> Points;
            
        }
        private int b;
        public int B { get { return b; } set { b = value; OnPropertyChanged(); } }


        private int t;
        public int T { get { return t; } set { t = value; OnPropertyChanged(); } }


        private int j;
        public int J { get { return j; } set { j = value; OnPropertyChanged(); } }

        private int h;
        public int H { get { return h; } set { h = value; OnPropertyChanged(); } }


        private int w;
        public int W { get { return w; } set { w = value; OnPropertyChanged(); } }


        private int n;
        public int N { get { return n; } set { n = value; OnPropertyChanged(); } }

        private double res;
        public double Result { get { return res; } set { res = value; OnPropertyChanged(); } }

        private int lowerValue;
        public int LowerValue
        {
            get { return lowerValue; }
            set { lowerValue = value; OnPropertyChanged(); }
        }
        private int upperValue;
        public int UpperValue
        {
            get { return upperValue; }
            set { upperValue = value; OnPropertyChanged(); }
        }

        private readonly RelayCommand mSolve;
        public ICommand SolveCommand { get { return mSolve; } }

        ModelIzinga model;

        public GraphicModel eModel { get; set; }
        public GraphicModel uModel { get; set; }
        public GraphicModel cModel { get; set; }
        public GraphicModel mModel { get; set; }

        public ViewModel()
        {
            T = 1;
            J = 1;
            B = 0;
            H = 5;
            W = 5;

            N = 22;
            LowerValue = 1;
            UpperValue = 10;
            eModel = new GraphicModel("E(T)", "E", "Дж");
            uModel = new GraphicModel("U(T)", "E", "Дж");
            cModel = new GraphicModel("C(T)", "E", "Дж");
            mModel = new GraphicModel("M(T)", "E", "Дж");

            Solve(null);
            mSolve = new RelayCommand(Solve);
        }

        private void InitPlotModel()
        {

            //EPlotModel = new PlotModel();
            //UPlotModel = new PlotModel();
            //CPlotModel = new PlotModel();
            //MPlotModel = new PlotModel();

            //FirstSeries = new LineSeries();
            //U_FirstSeries = new LineSeries();
            //C_FirstSeries = new LineSeries();
            //M_FirstSeries = new LineSeries();
        }

        private void LoadData()
        {
            //    FirstSeries.Points.Clear();
            //    U_FirstSeries.Points.Clear();
            //    C_FirstSeries.Points.Clear();
            //    M_FirstSeries.Points.Clear();

            //    EPlotModel.Axes.Clear();
            //    UPlotModel.Axes.Clear();
            //    CPlotModel.Axes.Clear();
            //    MPlotModel.Axes.Clear();

            //    EPlotModel.Series.Clear();
            //    UPlotModel.Series.Clear();
            //    CPlotModel.Series.Clear();
            //    MPlotModel.Series.Clear();

            //    X = new LinearAxis()
            //    {
            //        Position = AxisPosition.Bottom,
            //        Minimum = epoints.Min(x => x.X) - 1,
            //        Maximum = epoints.Max(x => x.X) + 1
            //    };
            //    Y = new LinearAxis()
            //    {
            //        Position = AxisPosition.Left,
            //        IsPanEnabled = false,
            //        Minimum = epoints.Min(x => x.Y) - 1,
            //        Maximum = epoints.Max(x => x.Y) + 1
            //    };

            //    EPoints.ForEach(x =>
            //    {
            //        FirstSeries.Points.Add(new DataPoint(x.X, x.Y));
            //    });
            //    EPlotModel.Series.Add(FirstSeries);

            //    EPlotModel.Axes.Add(X);
            //    EPlotModel.Axes.Add(Y);

            //    EPlotModel.InvalidatePlot(true);
        }

        private void Solve(object obj)
        {
            model = new ModelIzinga(W, H);
            Result = model.Run(T, J, B).Item2;

            eModel.Points.Clear();
            uModel.Points.Clear();
            cModel.Points.Clear();
            mModel.Points.Clear();

            double x_point = lowerValue;
            double step = 1.0;
            Tuple<double, double, double> ucm = new Tuple<double, double, double>(0, 0, 0);
            while (x_point <= upperValue)
            {
                eModel.Points.Add(new Point(x_point, model.Run(x_point, J, B).Item2));

                ucm = model.CalculateDynamicValues(x_point);

                uModel.Points.Add(new Point(x_point, ucm.Item1));
                cModel.Points.Add(new Point(x_point, ucm.Item2));
                mModel.Points.Add(new Point(x_point, ucm.Item3));

                //Add points for other models

                x_point += step;
            }

            eModel.Update();
            uModel.Update();
            cModel.Update();
            mModel.Update();
        }
    }
}
