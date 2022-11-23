using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowModel
{
    class ContainerEl
    {
        public Ellipse el { get; set; }
        public string nameColor { get; set; }
        public DoubleAnimation animation;
        public ColorAnimation color;
        public Storyboard storyboard;
        public EventHandler handler1;
        public EventHandler handler2;
    }

    /// <summary>
    /// Логика взаимодействия для Model2D.xaml
    /// </summary>
    public partial class Model2D : Window
    {
        public Model2D(double speedCap)
        {
            InitializeComponent();
            this.speedCap = speedCap;
        }

        private double speedCap;
        private Rectangle rect;
        private List<ContainerEl> collectionEl;
        private bool EndAnimatiom = false;
        private static object FieldCurrent;
        private static FrameworkElement elCurrent;

        public EventHandler handler;
        public EventHandler handler2;

        public void MoveTo(FrameworkElement el, FrameworkElement rect, string nameBrush)
        {
            if (EndAnimatiom == false)
            {
                double speed = (1 / (Canvas.GetTop(el) / rect.Height)) * speedCap;

                if (speed == 0)
                {
                    speed = 0.1;
                }

                double start = Canvas.GetLeft(el);

                double end = 400;

                double time = (end - start) / speed;

                var animation = new DoubleAnimation
                {
                    From = start,
                    To = end - 9,
                    Duration = new Duration(TimeSpan.FromSeconds(time)),
                    FillBehavior = FillBehavior.HoldEnd
                    //RepeatBehavior = RepeatBehavior.Forever

                };

                Storyboard.SetTarget(animation, el);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));


                var color1 = new ColorAnimation
                {
                    To = Color.FromRgb(235, 23, 12),
                    Duration = new Duration(TimeSpan.FromSeconds(time)),
                    FillBehavior = FillBehavior.HoldEnd
                };

                collectionEl.FirstOrDefault(x => x.el == el).animation = animation;
                collectionEl.FirstOrDefault(x => x.el == el).color = color1;

                //Storyboard.SetTarget(color1, el);
                Storyboard.SetTargetName(color1, nameBrush);
                Storyboard.SetTargetProperty(color1, new PropertyPath(SolidColorBrush.ColorProperty));

                handler = (s, e) => Field.Children.Remove(el);
                handler2 = (s, e) =>
                  {
                      Ellipse newEl = new Ellipse();
                      newEl.Name = "el";
                      newEl.Width = 8;
                      newEl.Height = 8;

                      SolidColorBrush redBrush = new SolidColorBrush();
                      redBrush.Color = Color.FromRgb(235, 145, 12);

                      newEl.Fill = redBrush;
                      Random rnd1 = new Random();
                      string newNameBrush = nameBrush + rnd1.Next(1201, 10000);
                      this.RegisterName(newNameBrush, redBrush);
                      newEl.HorizontalAlignment = HorizontalAlignment.Left;
                      newEl.VerticalAlignment = VerticalAlignment.Top;

                      Random rnd = new Random();

                      Canvas.SetLeft(newEl, 0);
                      Canvas.SetTop(newEl, Canvas.GetTop(el));

                      Field.Children.Add(newEl);
                      collectionEl.Remove(collectionEl.FirstOrDefault(x => x.el == el));
                      if (collectionEl is not null && el is not null && this.FindName(nameBrush) is not null)
                          this.UnregisterName(nameBrush);

                      collectionEl.Add(new ContainerEl { el = newEl, nameColor = newNameBrush, animation = animation, color = color1 });

                      MoveTo(newEl, rect, newNameBrush);
                  };

                collectionEl.FirstOrDefault(x => x.el == el).handler1 = handler;
                collectionEl.FirstOrDefault(x => x.el == el).handler2 = handler2;
                animation.Completed += handler;
                animation.Completed += handler2;
                //el.BeginAnimation(Canvas.LeftProperty, animation);

                Storyboard sb = new Storyboard();
                sb.Children.Add(animation);
                sb.Children.Add(color1);
                collectionEl.FirstOrDefault(x => x.el == el).storyboard = sb;
                sb.Begin(this);
            }

        }


        public void StopAnimation(FrameworkElement el, FrameworkElement rect, string nameBrush, DoubleAnimation animationEnd, ColorAnimation colorEnd, Storyboard sb)
        {
            //sb.ClearValue(Canvas.LeftProperty);
            //sb.ClearValue(SolidColorBrush.ColorProperty);


            //el.BeginAnimation(Canvas.LeftProperty, null);
            //el.BeginAnimation(SolidColorBrush.ColorProperty, null);

            //if (animationEnd != null)
            //{
            //    animationEnd.Completed -= (s, e) => Field.Children.Remove(el);
            //    animationEnd.Completed -= (s, e) =>
            //    {
            //        collectionEl.Remove(collectionEl.FirstOrDefault(x => x.el == el));


            //        Ellipse newEl = new Ellipse();
            //        newEl.Name = "el";
            //        newEl.Width = 8;
            //        newEl.Height = 8;

            //        SolidColorBrush redBrush = new SolidColorBrush();
            //        redBrush.Color = Color.FromRgb(235, 145, 12);

            //        newEl.Fill = redBrush;
            //        Random rnd1 = new Random();
            //        string newNameBrush = nameBrush + rnd1.Next(1201, 10000);
            //        this.RegisterName(newNameBrush, redBrush);
            //        newEl.HorizontalAlignment = HorizontalAlignment.Left;
            //        newEl.VerticalAlignment = VerticalAlignment.Top;

            //        Random rnd = new Random();

            //        Canvas.SetLeft(newEl, 0);
            //        Canvas.SetTop(newEl, Canvas.GetTop(el));

            //        Field.Children.Add(newEl);

            //        collectionEl.Add(new ContainerEl { el = newEl, nameColor = newNameBrush });

            //        MoveTo(newEl, rect, newNameBrush);
            //    };
            //}

            double start = Canvas.GetLeft(el);

            animationEnd = new DoubleAnimation
            {
                From = start,
                To = start
            };


            animationEnd.Completed -= handler;
            animationEnd.Completed -= handler2;

            Storyboard.SetTarget(animationEnd, el);
            Storyboard.SetTargetProperty(animationEnd, new PropertyPath(Canvas.LeftProperty));


            colorEnd = new ColorAnimation
            {
                From = (Color?)((Ellipse)el).Fill.GetValue(SolidColorBrush.ColorProperty),
                To = (Color?)((Ellipse)el).Fill.GetValue(SolidColorBrush.ColorProperty)
            };

            //Storyboard.SetTarget(color1, el);
            Storyboard.SetTargetName(colorEnd, nameBrush);
            Storyboard.SetTargetProperty(colorEnd, new PropertyPath(SolidColorBrush.ColorProperty));
            //el.BeginAnimation(Canvas.LeftProperty, animation);

            sb = new Storyboard();

            sb.Children.Add(animationEnd);
            sb.Children.Add(colorEnd);

            sb.Begin(this, true);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            rect = new Rectangle();
            rect.Name = "Rect";
            rect.Width = 400;
            rect.Height = 200;
            rect.Fill = new SolidColorBrush(Color.FromRgb(232, 173, 95));
            rect.Stroke = new SolidColorBrush(Color.FromRgb(103, 58, 183));



            Canvas.SetLeft(rect, 0);
            Canvas.SetBottom(rect, 0);

            Field.Children.Add(rect);

            collectionEl = new List<ContainerEl>();

            for (int i = 0; i < 1200; i++)
            {
                Ellipse el = new Ellipse();
                el.Name = "el";
                el.Width = 8;
                el.Height = 8;

                SolidColorBrush redBrush = new SolidColorBrush();
                redBrush.Color = Color.FromRgb(235, 145, 12);

                el.Fill = redBrush;
                this.RegisterName($"redBrush{i}", redBrush);
                el.HorizontalAlignment = HorizontalAlignment.Left;
                el.VerticalAlignment = VerticalAlignment.Top;

                Random rnd = new Random();

                Canvas.SetLeft(el, rnd.Next(0, (int)rect.Width - (int)el.Width));
                Canvas.SetTop(el, rnd.Next(0, (int)rect.Height - (int)el.Height));

                Field.Children.Add(el);
                collectionEl.Add(new ContainerEl { el = el, nameColor = $"redBrush{i}" });

                //MoveTo(el, rect, $"redBrush{i}");
            }

            speedTb.Value = speedCap;
        }

        private void speedTb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (speedTb.Value == null)
                return;
            speedCap = (double)speedTb.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Content == "Запустить")
            {
                EndAnimatiom = false;
                ((Button)sender).Content = "Остановить";
                speedTb.IsReadOnly = true;

                foreach (var item in collectionEl)
                {
                    MoveTo(item.el, rect, item.nameColor);
                }
            }

            else
            {
                ((Button)sender).Content = "Запустить";
                EndAnimatiom = true;
                speedTb.IsReadOnly = false;

                //color1.BeginTime = null;
                //animation.BeginTime = null;

                foreach (var item in collectionEl)
                {
                    item.animation.Completed -= item.handler1;
                    item.animation.Completed -= item.handler2;
                    Field.Children.Remove(item.el);
                    this.UnregisterName(item.nameColor);
                }

                collectionEl.Clear();

                collectionEl = new List<ContainerEl>();

                Random random = new Random();
                int index = random.Next(1, 100);

                for (int i = 0; i < 1200; i++)
                {
                    Ellipse el = new Ellipse();
                    el.Name = "el";
                    el.Width = 8;
                    el.Height = 8;

                    SolidColorBrush redBrush = new SolidColorBrush();
                    redBrush.Color = Color.FromRgb(235, 145, 12);

                    el.Fill = redBrush;

                    this.RegisterName($"redBrush{i}{index}", redBrush);
                    el.HorizontalAlignment = HorizontalAlignment.Left;
                    el.VerticalAlignment = VerticalAlignment.Top;

                    Random rnd = new Random();

                    Canvas.SetLeft(el, rnd.Next(0, (int)rect.Width - (int)el.Width));
                    Canvas.SetTop(el, rnd.Next(0, (int)rect.Height - (int)el.Height));

                    Field.Children.Add(el);
                    collectionEl.Add(new ContainerEl { el = el, nameColor = $"redBrush{i}{index}" });

                    //MoveTo(el, rect, $"redBrush{i}");
                }
            }
        }
    }
}
