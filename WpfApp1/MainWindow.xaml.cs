using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateAPolygon();
        }

        private void CreateAPolygon()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\furkanyilmaz\source\repos\WpfApp1\jsonFile.txt");

            JArray jsonResponse = JArray.Parse(text);

            List<MyShape> shapesToDraw = new List<MyShape>();

            foreach (JObject item in jsonResponse)
            {
                string value = item["type"].ToString();
                if (value == "line")
                {
                    string pointA = (string)item["a"];
                    string pointB = (string)item["b"];

                    string colors = (string)item["color"];

                    Line line = new Line();
                    line.A = pointA;
                    line.B = pointB;
                    line.Color = colors;
                    line.Type = value; // todo;

                    shapesToDraw.Add(line);

                }
                else if (value == "circle")
                {
                    MyCircle myCircle = new MyCircle();

                    string radius = (string)item["radius"];
                    string center = (string)item["center"];

                    myCircle.Filled = (bool)item["filled"];
                    myCircle.Type = value; // todo;
                    myCircle.Radius = radius; // todo;
                    myCircle.Center = center; // todo;
                    shapesToDraw.Add(myCircle);
                }
                else if (value == "rectangle")
                {

                    string pointA = (string)item["a"];
                    string pointB = (string)item["b"];
                    string pointC = (string)item["c"];
                    string pointD = (string)item["d"];

                    string colors = (string)item["color"];

                    MyRectangle rectangle = new MyRectangle();
                    rectangle.A = pointA;
                    rectangle.B = pointB;
                    rectangle.C = pointC;
                    rectangle.D = pointD;
                    rectangle.Color = colors;
                    rectangle.Type = value; // todo;
                    rectangle.Filled = (bool)item["filled"];


                    shapesToDraw.Add(rectangle);

                }
                else if (value == "triangle")
                {

                    SolidColorBrush testBrush = new SolidColorBrush();

                    string colors = (string)item["color"];
                    Byte[] words = colors.Split(';').Select(Byte.Parse).ToArray();

                    testBrush.Color = Color.FromArgb(words[0], words[1], words[2], words[3]);

                    string pointA = (string)item["a"];
                    string pointB = (string)item["b"];
                    string pointC = (string)item["c"];


                    float[] xyPointsA = pointA.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsB = pointB.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsC = pointC.Split(';').Select(float.Parse).ToArray();

                    System.Windows.Point PointATest = new System.Windows.Point(xyPointsA[0], xyPointsA[1]);
                    System.Windows.Point PointBTest = new System.Windows.Point(xyPointsB[0], xyPointsB[1]);
                    System.Windows.Point PointCTest = new System.Windows.Point(xyPointsC[0], xyPointsC[1]);


                    Triangle triangle = new Triangle();
                    triangle.A = pointA;
                    triangle.B = pointB;
                    triangle.C = pointC;

                    triangle.WindowsColor = testBrush.Color;
                    triangle.Color = colors;
                    triangle.Type = value;
                    triangle.Filled = (bool)item["filled"];

                    shapesToDraw.Add(triangle);
                }

            }

            drawShape(shapesToDraw);
        }
        private void MyHandler(object sender, MouseButtonEventArgs e)
        {

            MessageBox.Show("Hello, world!", "My App");

            Console.WriteLine("testsetsetests");
            Trace.WriteLine("testsetsetests");

        }

        void buttonNext_Click(object sender, EventArgs e, string index)
        {
            MessageBox.Show("Hello, world!", index);
            //your code
        }

        private void AddRectangle()
        {

            SolidColorBrush yellowBrush = new SolidColorBrush();
            yellowBrush.Color = Colors.Yellow;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            // Create a Polygon  
            Polygon rectangle = new Polygon();
            rectangle.Stroke = blackBrush;
            rectangle.Fill = yellowBrush;
            rectangle.StrokeThickness = 4;
            // Create a collection of points for a polygon  
            System.Windows.Point Point1 = new System.Windows.Point(750, 10);
            System.Windows.Point Point2 = new System.Windows.Point(750, 110);
            System.Windows.Point Point3 = new System.Windows.Point(550, 10);
            System.Windows.Point Point4 = new System.Windows.Point(550, 10);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);
            polygonPoints.Add(Point4);
            // Set Polygon.Points properties  
            rectangle.Points = polygonPoints;

            // Add Polygon to the page  
            LayoutRoot.Children.Add(rectangle);

        }

        private void drawShape(List<MyShape> shapesToDraw)
        {
            foreach (var item in shapesToDraw)
            {
                if (item.Type == "triangle")
                {
                    Triangle currentShape = (Triangle)item;
                    SolidColorBrush yellowBrush = new SolidColorBrush();

                    Byte[] colorsARGB = item.Color.Split(';').Select(Byte.Parse).ToArray();
                    yellowBrush.Color = Color.FromArgb(colorsARGB[0], colorsARGB[1], colorsARGB[2], colorsARGB[3]);

                    SolidColorBrush blackBrush = new SolidColorBrush();
                    blackBrush.Color = Colors.Black;
                    // Create a Polygon  
                    Polygon yellowPolygon = new Polygon();
                    yellowPolygon.Stroke = blackBrush;
                    if (currentShape.Filled)
                    {
                        yellowPolygon.Fill = yellowBrush;
                    }
                    yellowPolygon.StrokeThickness = 1;

                    // Create a collection of points for a polygon  

                    float[] xyPointsA = currentShape.A.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsB = currentShape.B.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsC = currentShape.C.Split(';').Select(float.Parse).ToArray();

                    System.Windows.Point Point1 = new System.Windows.Point(300 + xyPointsA[0], 300 + xyPointsA[1]);
                    System.Windows.Point Point2 = new System.Windows.Point(300 + xyPointsB[0], 300 + xyPointsB[1]);
                    System.Windows.Point Point3 = new System.Windows.Point(300 + xyPointsC[0], 300 + xyPointsC[1]);

                    PointCollection polygonPoints = new PointCollection();
                    polygonPoints.Add(Point1);
                    polygonPoints.Add(Point2);
                    polygonPoints.Add(Point3);

                    // Set Polygon.Points properties  
                    yellowPolygon.Points = polygonPoints;

                    //yellowPolygon.MouseLeftButtonDown += new MouseButtonEventHandler(MyHandler);
                    yellowPolygon.MouseLeftButtonDown += (sender, EventArgs) => { shapeEventHandler(sender, EventArgs, currentShape); };


                    // Add Polygon to the page  
                    LayoutRoot.Children.Add(yellowPolygon);

                }
                else if (item.Type == "rectangle")
                {
                    MyRectangle currentShape = (MyRectangle)item;
                    SolidColorBrush innerBrush = new SolidColorBrush();

                    Byte[] colorsARGB = item.Color.Split(';').Select(Byte.Parse).ToArray();
                    innerBrush.Color = Color.FromArgb(colorsARGB[0], colorsARGB[1], colorsARGB[2], colorsARGB[3]);

                    SolidColorBrush blackBrush = new SolidColorBrush();
                    blackBrush.Color = Colors.Black;
                    // Create a Polygon  
                    Polygon rectanglePolygon = new Polygon();
                    rectanglePolygon.Stroke = blackBrush;
                    if (currentShape.Filled)
                    {
                        rectanglePolygon.Fill = innerBrush;
                    }
                    rectanglePolygon.StrokeThickness = 2;

                    // Create a collection of points for a polygon  

                    float[] xyPointsA = currentShape.A.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsB = currentShape.B.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsC = currentShape.C.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsD = currentShape.D.Split(';').Select(float.Parse).ToArray();

                    System.Windows.Point Point1 = new System.Windows.Point(300 + xyPointsA[0], 300 + xyPointsA[1]);
                    System.Windows.Point Point2 = new System.Windows.Point(300 + xyPointsB[0], 300 + xyPointsB[1]);
                    System.Windows.Point Point3 = new System.Windows.Point(300 + xyPointsC[0], 300 + xyPointsC[1]);
                    System.Windows.Point Point4 = new System.Windows.Point(300 + xyPointsD[0], 300 + xyPointsD[1]);

                    PointCollection polygonPoints = new PointCollection();
                    polygonPoints.Add(Point1);
                    polygonPoints.Add(Point2);
                    polygonPoints.Add(Point3);
                    polygonPoints.Add(Point4);

                    // Set Polygon.Points properties  
                    rectanglePolygon.Points = polygonPoints;

                    //yellowPolygon.MouseLeftButtonDown += new MouseButtonEventHandler(MyHandler);
                    rectanglePolygon.MouseLeftButtonDown += (sender, EventArgs) => { shapeEventHandler(sender, EventArgs, currentShape); };


                    // Add Polygon to the page  
                    LayoutRoot.Children.Add(rectanglePolygon);


                }
                else if (item.Type == "line")
                {
                    Line currentShape = (Line)item;
                    SolidColorBrush edgeBrush = new SolidColorBrush();

                    Byte[] colorsARGB = item.Color.Split(';').Select(Byte.Parse).ToArray();
                    edgeBrush.Color = Color.FromArgb(colorsARGB[0], colorsARGB[1], colorsARGB[2], colorsARGB[3]);

                    edgeBrush.Color = Colors.Black;


                    Polygon linePolygon = new Polygon();
                    linePolygon.Stroke = edgeBrush;

                    linePolygon.StrokeThickness = 5;

                    // Create a collection of points for a polygon  

                    float[] xyPointsA = currentShape.A.Split(';').Select(float.Parse).ToArray();
                    float[] xyPointsB = currentShape.B.Split(';').Select(float.Parse).ToArray();


                    System.Windows.Point Point1 = new System.Windows.Point(300 + xyPointsA[0], 300 + xyPointsA[1]);
                    System.Windows.Point Point2 = new System.Windows.Point(300 + xyPointsB[0], 300 + xyPointsB[1]);

                    PointCollection polygonPoints = new PointCollection();
                    polygonPoints.Add(Point1);
                    polygonPoints.Add(Point2);

                    // Set Polygon.Points properties  
                    linePolygon.Points = polygonPoints;

                    //yellowPolygon.MouseLeftButtonDown += new MouseButtonEventHandler(MyHandler);
                    linePolygon.MouseLeftButtonDown += (sender, EventArgs) => { shapeEventHandler(sender, EventArgs, currentShape); };

                    // Add Polygon to the page  
                    LayoutRoot.Children.Add(linePolygon);

                }
                else if (item.Type == "circle")
                {
                    Ellipse circle = new Ellipse();
                    MyCircle currentShape = (MyCircle)item;
                    // Create a SolidColorBrush with a red color to fill the
                    // Ellipse with.
                    SolidColorBrush mySolidColorBrush2 = new SolidColorBrush();

                    // Describes the brush's color using RGB values.
                    // Each value has a range of 0-255.
                    mySolidColorBrush2.Color = Color.FromArgb(255, 255, 0, 0);
                    circle.Fill = mySolidColorBrush2;
                    circle.StrokeThickness = 2;
                    circle.Stroke = Brushes.Navy;

                    // Set the width and height of the Ellipse.
                    //circle.Width = 100;
                    float radiusFloat = float.Parse(currentShape.Radius);
                    circle.Width = radiusFloat * 2;
                    //circle.Height = 100;
                    circle.Height = radiusFloat * 2;
                    float[] centerPoints = currentShape.Center.Split(';').Select(float.Parse).ToArray();
                    float centerX = centerPoints[0];
                    float centerY = centerPoints[1];

                    circle.Margin = new Thickness(centerX, centerY, 0, 0);

                    circle.MouseLeftButtonDown += (sender, EventArgs) => { shapeEventHandler(sender, EventArgs, currentShape); };

                    LayoutRoot.Children.Add(circle);
                }

            }
        }

        void shapeEventHandler(object sender, EventArgs e, MyShape shape)
        {
            String message = "";
            if (shape.Type == "triangle")
            {
                Triangle triangle = (Triangle)shape;
                message = "Edge A: " + triangle.A + System.Environment.NewLine +
                    "Edge B: " + triangle.B + System.Environment.NewLine +
                    "Edge C: " + triangle.C + System.Environment.NewLine +
                    "Color: " + triangle.Color;
            }
            else if (shape.Type == "rectangle")
            {
                MyRectangle myRectangle = (MyRectangle)shape;
                message = "Edge A: " + myRectangle.A + System.Environment.NewLine +
                    "Edge B: " + myRectangle.B + System.Environment.NewLine +
                    "Edge C: " + myRectangle.C + System.Environment.NewLine +
                    "Edge D: " + myRectangle.D + System.Environment.NewLine +
                    "Color: " + myRectangle.Color;
            }
            else if (shape.Type == "line")
            {
                Line line = (Line)shape;
                message = "Edge A: " + line.A + System.Environment.NewLine +
                    "Edge B: " + line.B + System.Environment.NewLine +
                    "Color: " + line.Color;
            }
            else if (shape.Type == "circle")
            {
                MyCircle myCircle = (MyCircle)shape;
                message = "Center: " + myCircle.Center + System.Environment.NewLine +
                    "Radius: " + myCircle.Radius + System.Environment.NewLine +
                    "Color: " + myCircle.Color;
            }
            MessageBox.Show(message, shape.Type.ToUpper());
            //your code
        }

    }

}
