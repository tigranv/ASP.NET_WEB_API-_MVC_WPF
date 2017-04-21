﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Net.Http.Formatting;



namespace GraphPlotApiClientWPF
{

    public partial class MainWindow : Window
    {
        private Polyline polyline;

        public MainWindow()
        {
            polyline = new Polyline { Stroke = Brushes.Black };
            InitializeComponent();
        }

        private void Draw_Graph_Click(object sender, RoutedEventArgs e)
        {
            RequestParameters param = new RequestParameters();
        
            if ((Boolean)SinRadioBt.IsChecked)
            {
                param.Amplitude = double.Parse(AmplitudeTexXox.Text);
                param.frequency = double.Parse(FrequencyTextBox.Text);
                AddChart(SinRadioBt.Name, param);
            }
            else if ((Boolean)CosRadioBt.IsChecked)
            {
                param.Amplitude = double.Parse(AmplitudeTexXox.Text);
                param.frequency = double.Parse(FrequencyTextBox.Text);
                AddChart(CosRadioBt.Name, param);
            }
            else if ((Boolean)PowRadioBt.IsChecked)
            {
                param.N = int.Parse(PowN.Text);
                AddChart(PowRadioBt.Name, param);
            }
            else if ((Boolean)LogRadioBt.IsChecked)
            {
                param.X = double.Parse(LogX.Text);
                AddChart(LogRadioBt.Name, param);
            }
            else
            {
                
            }

        }

        private void AddChart(string FuncName, RequestParameters parameters)
        {
            HttpClient client = new HttpClient();

            string url = string.Format("http://localhost:53578/api/PlotGraph?function={0}",  Uri.EscapeDataString(FuncName));

            client.PostAsync(url, parameters, new JsonMediaTypeFormatter())
               .ContinueWith(response =>
               {
                   if (response.Exception != null)
                   {
                       MessageBox.Show(response.Exception.Message);
                   }
                   else
                   {
                       JavaScriptSerializer jss = new JavaScriptSerializer();
                       HttpResponseMessage message = response.Result;
                       string responseText = message.Content.ReadAsStringAsync().Result;
                       List<Point> list = jss.Deserialize<List<Point>>(responseText);

                       Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                           (Action)(() =>
                           {
                               foreach (var item in list)
                               {
                                   polyline.Points.Add(CorrespondingPoint(new Point(item.X, item.Y)));
                               }
                               canvas.Children.Remove(polyline);
                               canvas.Children.Add(polyline);
                           }));
                   }
               });
        }

        private Point CorrespondingPoint(Point pt)
        {
        double xmin = 0;
        double xmax = 6.5;
        double ymin = -1.1;
        double ymax = 1.1;

        var result = new Point
            {
                X = (pt.X - xmin) * canvas.Width / (xmax - xmin),
                Y = canvas.Height - (pt.Y - ymin) * canvas.Height / (ymax - ymin)
            };
            return result;
        }

        private void ClearButtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


//client.GetAsync(url)
//   .ContinueWith(response =>
//   {
//       if (response.Exception != null)
//       {
//           MessageBox.Show(response.Exception.Message);
//       }
//       else
//       {
//           HttpResponseMessage message = response.Result;
//           string responseText = message.Content.ReadAsStringAsync().Result;
//           List<Point> list = jss.Deserialize<List<Point>>(responseText);

//           Dispatcher.BeginInvoke(DispatcherPriority.Normal,
//               (Action)(() =>
//               {
//                   foreach (var item in list)
//                   {
//                       polyline.Points.Add(CorrespondingPoint(new Point(item.X, item.Y)));
//                   }
//                   canvas.Children.Add(polyline);
//               }));
//       }
//   });