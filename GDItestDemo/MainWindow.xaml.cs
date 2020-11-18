using System;
using System.Collections.Generic;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private bool isDown = false;
        PathGeometry gridGeometry = new PathGeometry();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
        }
          
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //CurEntPoint.Content = e.GetPosition(this);
            //if (isDown)
            //{
            //    EllipseGeometry rg = new EllipseGeometry();
            //    Point CurrentPoint = e.GetPosition(this);
            //    rg.Center = new Point(CurrentPoint.X, CurrentPoint.Y-50);
            //    rg.RadiusX = 10;
            //    rg.RadiusY = 10;
            //    //排除几何图形
            //    gridGeometry = Geometry.Combine(gridGeometry, rg, GeometryCombineMode.Exclude, null);
            //    grid.Clip = gridGeometry;
            //    pathGeometries.Add(gridGeometry);
               

            //}
            
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
        }
        public List<PathGeometry> pathGeometries = new List<PathGeometry>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RectangleGeometry rg = new RectangleGeometry();
            //rg.Rect = new Rect(0, 0, DXgdi.ActualWidth, DXgdi.ActualHeight);
            //gridGeometry = Geometry.Combine(gridGeometry, rg, GeometryCombineMode.Union, null);
            //grid.Clip = gridGeometry;
            //pathGeometries.Add(gridGeometry);
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //int i = 0;
            //foreach (var item in gridGeometry.Figures)
            //{
            //    i++;
            //    Point point = item.StartPoint;
            //    Console.WriteLine("坐标:"+i);
            //    Console.WriteLine(point);
            //    EllipseGeometry rg = new EllipseGeometry();
                  
            //}

          
        }

       
    }
}
