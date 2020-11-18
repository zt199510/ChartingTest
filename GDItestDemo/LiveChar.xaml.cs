using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GDItestDemo
{
    /// <summary>
    /// LiveChar.xaml 的交互逻辑
    /// </summary>
    public partial class LiveChar : UserControl
    {
        public LiveChar()
        {
            InitializeComponent();
            DataSourse = GetCollection();
        }
        private PointCollection GetCollection()
        {
            PointCollection myPointCollection = new PointCollection()
            {
                new Point(0,50),
                new Point(10,50),
                new Point(20,50),
                new Point(100,100)
            };

            return myPointCollection;
        }
        /// <summary>
        /// 画板宽度
        /// </summary>
        public double BoardWidth { get; set; }
        /// <summary>
        /// 画板高度
        /// </summary>
        public double BoardHeight { get; set; }
        /// <summary>
        /// 平行（横向）边距（画图区域距离左右两边长度）
        /// </summary>
        public double HorizontalMargin { get; set; }
        /// <summary>
        /// 垂直（纵向）边距（画图区域距离左右两边长度）
        /// </summary>
        public double VerticalMargin { get; set; }
        /// <summary>
        /// 水平刻度间距像素
        /// </summary>
        public double horizontalBetween { get; set; }
        /// <summary>
        /// 垂直刻度间距像素
        /// </summary>
        public double verticalBetween { get; set; }
        /// <summary>
        /// 图表区域宽度
        /// </summary>
        public double ChartWidth;
        /// <summary>
        /// 图表区域高度
        /// </summary>
        public double ChartHeight;
        /// <summary>
        /// <summary>
        /// 坐标点数据源
        /// </summary>
        public PointCollection DataSourse;
        /// <summary>
        /// 画图区域起点
        /// </summary>
        public Point StartPostion;
        /// <summary>
        /// 画图区域终点
        /// </summary>
        public Point EndPostion;
        /// <summary>
        ///     x轴最大值
        /// </summary>
        public double MaxX { get; set; }
        /// <summary>
        ///     y轴最大值
        /// </summary>
        public double MaxY { get; set; }
        /// <summary>
        ///     x轴最小值
        /// </summary>
        public double MinX { get; set; }
        /// <summary>
        ///     y轴最小值
        /// </summary>
        public double MinY { get; set; }
        double MapLocationX = 0;
        double MapLocationY = 0;
        private void LiveChar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            InitCanvas();

            //获取y最大值
            if (MaxY < 0.0001)
            {
                MaxY = 100;
            }
            //MinY = DataSourse.Min(m => m.Y);

            if (MaxX < 0.0001)
            {
                MaxX = 100;
            }
            //MinX = DataSourse.Min(m => m.X);
            if (Math.Abs(MaxX) < 0.000001 || Math.Abs(MaxY) < 0.000001)
            {
                return;
            }
            DrawAxis();
            DrawXAxisScale();
            DrawYAxisScale();
            DrawPolyLine();
        }
        List<Point> Points = new List<Point>();

        /// <summary>
        /// 初始化计算点的相对坐标
        /// </summary>
        private void DrawPolyLine()
        {


            foreach (var item in DataSourse)
            {

                Point point = GetRealPoint(item);
                Points.Add(point);
            }

            PaintLine(Points);
            PaintEllipse(Points);
        }
        /// <summary>
        ///  绘制控制点
        /// </summary>
        /// <param name="points"></param>
        private void PaintEllipse(List<Point> points)
        {
            int i = 0;
            foreach (var item in points)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = Brushes.Yellow;
                ellipse.Width = 5;
                ellipse.Height = 5;
                ellipse.Tag = i;
                MainCanvas.Children.Add(ellipse);
                Canvas.SetTop(ellipse, item.Y-(ellipse.Width/2));
                Canvas.SetLeft(ellipse, item.X - (ellipse.Height / 2));
                i++;
            }

        }

        Path path2 = null;
        /// <summary>
        /// h绘制曲线
        /// </summary>
        /// <param name="points"></param>
        private void PaintLine(List<Point> points, DrawType drawType = DrawType.C)
        {
            StringBuilder data = new StringBuilder("M");
            switch (drawType)
            {
                case DrawType.L:
                    data.AppendFormat("{0},{1} L", points[0].X, points[0].Y);
                    break;
                case DrawType.H:
                    data.AppendFormat("{0},{1} H", points[0].X, points[0].Y);
                    break;
                case DrawType.V:
                    data.AppendFormat("{0},{1} V", points[0].X, points[0].Y);
                    break;
                case DrawType.C:
                    data.AppendFormat("{0},{1} C", points[0].X, points[0].Y);
                    break;
                case DrawType.Q:
                    data.AppendFormat("{0},{1} Q", points[0].X, points[0].Y);
                    break;
                case DrawType.S:
                    data.AppendFormat("{0},{1} S", points[0].X, points[0].Y);
                    break;
                case DrawType.T:
                    data.AppendFormat("{0},{1} T", points[0].X, points[0].Y);
                    break;
                default:
                    break;
            }


            if (path2 != null)
            {

                for (int i = 1; i < points.Count; i++)
                {
                    Point pre;
                         Point next;
                    if (i == 1)
                    {
                        pre = new Point((points[i - 1].X + points[i].X) / 2, points[i ].Y);  //控制点
                        next = new Point( points[i].X , points[i].Y);     //控制点
                    }
                    else
                    {
                         pre = new Point((points[i - 1].X + points[i].X) / 2, points[i - 1].Y);  //控制点
                         next = new Point((points[i - 1].X + points[i].X) / 2, points[i].Y);     //控制点
                    }
                    //Point pre = new Point((points[i - 1].X + points[i].X) / 2, points[i - 1].Y);  //控制点
                    //Point next = new Point((points[i - 1].X + points[i].X) / 2, points[i - 1].Y);     //控制点
                
                    data.AppendFormat(" {0},{1} {2},{3} {4},{5}", pre.X, pre.Y, next.X, next.Y, points[i].X, points[i].Y);
                }
                path2.Data = Geometry.Parse(data.ToString());
            }
            else
            {

                //   data.AppendFormat("{0},{1} C", points[0].X, points[0].Y);
                for (int i = 1; i < points.Count; i++)
                {
                    Point pre = new Point((points[i - 1].X + points[i].X) / 2, points[i - 1].Y);  //控制点
                    Point next = new Point((points[i - 1].X + points[i].X) / 2, points[i].Y);     //控制点
                    data.AppendFormat(" {0},{1} {2},{3} {4},{5}", pre.X, pre.Y, next.X, next.Y, points[i].X, points[i].Y);
                }

                path2 = new Path { Stroke = Brushes.White, StrokeThickness = 2, Data = Geometry.Parse(data.ToString()) };

                this.MainCanvas.Children.Add(path2);
            }

        }
        /// <summary>
        /// 查询相对坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point GetRealPoint(Point point)
        {
            var realX = StartPostion.X + (point.X - MinX) * ChartWidth / (MaxX - MinX) + MapLocationX;
            var realY = StartPostion.Y + (MaxY - point.Y) * ChartHeight / (MaxY - MinY) + MapLocationY;
            return new Point(realX, realY);
        }
        /// <summary>
        /// 绘制Y刻度
        /// </summary>
        private void DrawYAxisScale()
        {
            if (MinY > MaxY) return;
            if (verticalBetween < 0.0001)
                verticalBetween = (MaxY - MinY) / 10;
            for (var i = MinY; i <= MaxY + 0.01; i += verticalBetween)
            {
                var y = EndPostion.Y - i * ChartHeight / (MaxY - MinY) + MapLocationY;
                var marker = new Line
                {
                    X1 = StartPostion.X - 5,
                    Y1 = y,
                    X2 = StartPostion.X,
                    Y2 = y,
                    Stroke = Brushes.Red
                };
                MainCanvas.Children.Add(marker);

                //绘制Y轴网格
                var gridLine = new Line
                {
                    X1 = StartPostion.X,
                    Y1 = y,
                    X2 = EndPostion.X,
                    Y2 = y,
                    StrokeThickness = 0.5,
                    Stroke = new SolidColorBrush(Colors.AliceBlue)
                };
                MainCanvas.Children.Add(gridLine);

                string text = i.ToString(CultureInfo.InvariantCulture);
                var MarkerText = new TextBlock
                {
                    Text = text,
                    Width = 30,
                    Foreground = Brushes.Yellow,
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    TextAlignment = TextAlignment.Right
                };
                MainCanvas.Children.Add(MarkerText);
                Canvas.SetTop(MarkerText, y - 10);
                Canvas.SetLeft(MarkerText, 00);

            }
        }

        /// <summary>
        /// 绘制X刻度
        /// </summary>
        private void DrawXAxisScale()
        {
            if (MinX >= MaxX) return;
            if (horizontalBetween < 0.0001)
                horizontalBetween = (MaxX - MinX) / 10;

            for (var i = MinX; i <= MaxX + 0.01; i += horizontalBetween)
            {
                var x = StartPostion.X + i * ChartWidth / (MaxX - MinX) + MapLocationX;

                ///绘制X轴刻度
                var marker = new Line
                {
                    X1 = x,
                    Y1 = EndPostion.Y,
                    X2 = x,
                    Y2 = EndPostion.Y + 4,
                    Stroke = Brushes.Red
                };
                MainCanvas.Children.Add(marker);

                //绘制X轴网格
                var gridLine = new Line
                {
                    X1 = x,
                    Y1 = StartPostion.Y,
                    X2 = x,
                    Y2 = EndPostion.Y,
                    StrokeThickness = 0.5,
                    Stroke = new SolidColorBrush(Colors.AliceBlue)
                };
                MainCanvas.Children.Add(gridLine);


                //绘制X轴字符
                var text = i.ToString(CultureInfo.InvariantCulture);

                var markText = new TextBlock
                {
                    Text = text,
                    Width = 130,
                    Foreground = Brushes.Yellow,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    TextAlignment = TextAlignment.Left,
                    FontSize = 10
                };
                MainCanvas.Children.Add(markText);
                Canvas.SetTop(markText, EndPostion.Y + 5);
                Canvas.SetLeft(markText, x - 5);

            }
        }

        /// <summary>
        /// 绘制线条
        /// </summary>
        private void DrawAxis()
        {
            var xaxis = new Line
            {
                X1 = StartPostion.X,
                Y1 = EndPostion.Y,
                X2 = EndPostion.X,
                Y2 = EndPostion.Y,
                Stroke = new SolidColorBrush(Colors.Black)

            };
            MainCanvas.Children.Add(xaxis);

            var XaxisTop = new Line
            {
                X1 = StartPostion.Y,
                Y1 = StartPostion.Y,
                X2 = EndPostion.X,
                Y2 = StartPostion.Y,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            MainCanvas.Children.Add(XaxisTop);
            var yaxis = new Line
            {
                X1 = StartPostion.X,
                Y1 = StartPostion.Y,
                X2 = StartPostion.X,
                Y2 = EndPostion.Y,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            MainCanvas.Children.Add(yaxis);

            var YaxisBottom = new Line
            {
                X1 = EndPostion.X,
                Y1 = EndPostion.Y,
                X2 = EndPostion.X,
                Y2 = StartPostion.Y,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            MainCanvas.Children.Add(YaxisBottom);

        }
        TextBox moushPonit;
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitCanvas()
        {
            MainCanvas.Children.Clear();

            BoardWidth = MainCanvas.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            BoardHeight = MainCanvas.ActualHeight - SystemParameters.HorizontalScrollBarHeight;
            HorizontalMargin = 40;
            VerticalMargin = 40;

            ChartWidth = BoardWidth - 2 * HorizontalMargin;//画图区域宽度
            ChartHeight = BoardHeight - 2 * VerticalMargin; //画图区域高度


            StartPostion = new Point(HorizontalMargin, VerticalMargin);
            EndPostion = new Point(BoardWidth - HorizontalMargin, BoardHeight - VerticalMargin);

            moushPonit = new TextBox
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Height = 20,
                Width = 200,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Top
            };
            MainCanvas.Children.Add(moushPonit);
            Canvas.SetTop(moushPonit, 0);
            Canvas.SetLeft(moushPonit, 0);
        }
        /// <summary>
        /// 查询当前速度与功率的值
        /// </summary>
        /// <param name="Point"></param>
        /// <returns></returns>
        public SpeedOrPower GetSpeedOrPower(Point Point)
        {
            double num2 = 100 - (Point.Y - VerticalMargin) / (EndPostion.Y - VerticalMargin) * 100;
            double num3 = (Point.X - HorizontalMargin) / (EndPostion.X - HorizontalMargin) * 100;
            double SpeedNum = Math.Round(num3, 1);
            double PowerNum = Math.Round(num2, 1);
         
            return new SpeedOrPower() {  SpeedNum=SpeedNum,PowerNum=PowerNum};
        }

        /// <summary>
        /// 鼠标移动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition((UIElement)sender);

            var CurrentPoint = GetSpeedOrPower(currentMousePosition);
            if (e.OriginalSource != null && e.OriginalSource.GetType() == typeof(Ellipse) && isMove)
            {

                Ellipse el = (Ellipse)e.OriginalSource;
                Point p = e.GetPosition(null);//获取鼠标移动中的坐标
                int index = Convert.ToInt32(el.Tag);
                if (CurrentPoint.SpeedNum < 0 || CurrentPoint.PowerNum < 0)
                    isMove = false;
                if (currentMousePosition.X > EndPostion.X || currentMousePosition.X < StartPostion.X)
                    isMove = false;
                if (eBefore.Y + (p.Y - pBefore.Y) > EndPostion.Y || eBefore.Y + (p.Y - pBefore.Y) < StartPostion.Y)
                    isMove = false;

                if (index == 0)
                {
                    var Getobj = GetSpeedOrPower(Points[index + 1]);
                    if(CurrentPoint.PowerNum>Getobj.PowerNum)
                    {
                        isMove = false; return;
                    }
                    Canvas.SetLeft(el, Points[index].X - (el.Width / 2));
                    Canvas.SetTop(el, eBefore.Y + (p.Y - pBefore.Y));
                    Points[index] = new Point(Points[index].X, currentMousePosition.Y);
                    PaintLine(Points);
                }
                else if (index == Points.Count() - 1)
                {

                    var Getobj = GetSpeedOrPower(Points[index -1]);
                    if (CurrentPoint.PowerNum < Getobj.PowerNum)
                    {
                        isMove = false; return;
                    }
                    Canvas.SetLeft(el, Points[index].X - (el.Width / 2));
                    Canvas.SetTop(el, eBefore.Y + (p.Y - pBefore.Y));
                    Points[index] = new Point(Points[index].X, currentMousePosition.Y);
                    PaintLine(Points);
                }
                else
                {
                        if (currentMousePosition.X > Points[index + 1].X) { isMove = false; return; }
                        if (currentMousePosition.Y < Points[index + 1].Y) { isMove = false; return; }
                        if (currentMousePosition.X < Points[index - 1].X) { isMove = false; return; }
                        if (currentMousePosition.Y > Points[index - 1].Y) { isMove = false; return; }
                 
                    Canvas.SetLeft(el, currentMousePosition.X-(el.Width/2));
                    Canvas.SetTop(el, currentMousePosition.Y - (el.Height / 2));
                    Points[index] = currentMousePosition;
                    PaintLine(Points);
                }

            }

            if (CurrentPoint.SpeedNum < 0)
            {
                moushPonit.Visibility = Visibility.Collapsed;
                CurrentPoint.SpeedNum = 0;
                return;
            }
            if (CurrentPoint.SpeedNum > 100)
            {
                moushPonit.Visibility = Visibility.Collapsed;
                return;
            }
            if (CurrentPoint.PowerNum < 0)
            {
                moushPonit.Visibility = Visibility.Collapsed;
                CurrentPoint.PowerNum = 0;
                return;
            }
            if (CurrentPoint.PowerNum > 100)
            {
                moushPonit.Visibility = Visibility.Collapsed;
                return;
            }
            moushPonit.Visibility = Visibility.Visible;
            moushPonit.Text = $"(Speed={CurrentPoint.SpeedNum}%:Power={CurrentPoint.PowerNum}%)";
            Canvas.SetTop(moushPonit, currentMousePosition.Y - 20);
            Canvas.SetLeft(moushPonit, currentMousePosition.X);

        
        }
        Point pBefore = new Point();//鼠标点击前坐标
        Point eBefore = new Point();//圆移动前坐标
        bool isMove = false;//是否需要移动

        /// <summary>
        /// 鼠标按下时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Ellipse))
            {
                this.pBefore = e.GetPosition(null);//获取点击前鼠标坐标
                Ellipse el = (Ellipse)e.OriginalSource;
                el.Width = el.Width + 5;
                el.Height = el.Height + 5;
                this.eBefore = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));//获取点击前圆的坐标
                isMove = true;//开始移动了
                el.CaptureMouse();//鼠标捕获此圆
            }
        }
        /// <summary>
        /// 鼠标释放时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Ellipse))
            {
                Ellipse el = (Ellipse)e.OriginalSource;
                int index = Convert.ToInt32(el.Tag);
                if (el.Width >= 10)
                {
                    el.Width = el.Width - 5;
                    el.Height = el.Height - 5;
                }

                Canvas.SetLeft(el, Points[index].X - (el.Width / 2));
                Canvas.SetTop(el, Points[index].Y - (el.Height / 2));
                isMove = false;//结束移动了
                el.ReleaseMouseCapture();//鼠标释放此圆

            }
        }
    }


    public enum DrawType
    {
        //Line
        L,
        //Horizontal line
        H,
        //Vertical line
        V,
        //Cubic Bezier curve
        C,
        //Quadratic Bezier curve
        Q,
        //Smooth cubic Bezier curve
        S,
        //smooth quadratic Bezier curve
        T
    }

    public class SpeedOrPower
    {

        public double SpeedNum { get; set; }
        public double PowerNum { get; set; }
    }
}
