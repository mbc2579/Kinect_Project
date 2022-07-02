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
using System.Speech.Synthesis;
using Microsoft.Kinect;

namespace Login2
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Game : Window
    {
        Rectangle[] m_rect = new Rectangle[20];
        SpeechSynthesizer ss = new SpeechSynthesizer();
        DateTime ThisMoment;
        DateTime tempThisMoment;
        TimeSpan duration;
        DateTime AfterWards;
        DateTime tempAfterWards;
        bool check = true;
        bool check2 = true;
        bool check3 = true;
        bool check4 = true;
        int chk = 0;
        int updown;
        int time;
        int count;
        string id;
        public Game(string id)
        {
            InitializeComponent();
            InitializeNui();
            this.id = id;
            for (int i = 0; i < 20; i++)
            {
                m_rect[i] = new Rectangle();
                m_rect[i].Fill = new SolidColorBrush(Colors.Green);
                m_rect[i].Height = 10;
                m_rect[i].Width = 10;
                m_rect[i].Visibility = Visibility.Visible;
                Canvas.SetTop(m_rect[i], 0);
                Canvas.SetLeft(m_rect[i], 0);
                canvas1.Children.Add(m_rect[i]);
            }
        }

        KinectSensor nui = null;

        void InitializeNui()
        {
            nui = KinectSensor.KinectSensors[0];

            nui.ColorStream.Enable();
            nui.ColorFrameReady += new
                EventHandler<ColorImageFrameReadyEventArgs>(nui_ColorFrameReady);

            nui.DepthStream.Enable();

            nui.SkeletonStream.Enable();
            nui.AllFramesReady += new
                EventHandler<AllFramesReadyEventArgs>(nui_AllFramesReady);
            nui.Start();
        }
        void nui_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame ImageParam = e.OpenColorImageFrame();

            if (ImageParam == null) return;

            byte[] ImageBits = new byte[ImageParam.PixelDataLength];

            ImageParam.CopyPixelDataTo(ImageBits);
            BitmapSource src = null;
            src = BitmapSource.Create(ImageParam.Width, ImageParam.Height, 96, 96, PixelFormats.Bgr32,
                                   null, ImageBits, ImageParam.Width * ImageParam.BytesPerPixel);
            image1.Source = src;
        }
        void nui_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            SkeletonFrame sf = e.OpenSkeletonFrame();
            if (sf == null) return;
            Skeleton[] skeletonData = new Skeleton[sf.SkeletonArrayLength];
            sf.CopySkeletonDataTo(skeletonData);
            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            {
                if (depthImageFrame != null)
                {
                    foreach (Skeleton sd in skeletonData)
                    {
                        if (sd.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            int temp;
                            int nMax = 20;
                            Joint[] joints = new Joint[nMax];
                            for (int j = 0; j < nMax; j++)
                            {
                                joints[j] = sd.Joints[(JointType)j];
                            }

                            Point[] points = new Point[nMax];
                            for (int j = 0; j < nMax; j++)
                            {
                                DepthImagePoint depthPoint;
                                depthPoint = depthImageFrame.MapFromSkeletonPoint(joints[j].Position);
                                points[j] = new Point((int)(image1.Width * depthPoint.X / depthImageFrame.Width),
                                    (int)(image1.Height * depthPoint.Y / depthImageFrame.Height));
                            }

                            for (int j = 0; j < nMax; j++)
                            {
                                m_rect[j].Visibility = Visibility.Visible;
                                Canvas.SetTop(m_rect[j], points[j].Y - (m_rect[j].Height / 2));
                                Canvas.SetLeft(m_rect[j], points[j].X - (m_rect[j].Width / 2));
                            }


                            if (check2)
                            {
                                ThisMoment = DateTime.Now;
                                duration = new TimeSpan(0, 0, 0, 0, 59000);
                                AfterWards = ThisMoment.Add(duration);
                                check2 = false;
                            }
                            ThisMoment = DateTime.Now;
                            time = (AfterWards - ThisMoment).Seconds;
                            if (time == 0) chk ++;
                            if (chk==1)
                            {
                                chk++;
                                System.Windows.MessageBox.Show("게임이 종료되었습니다.");
                                newRank newrecpage = new newRank(count, id);
                                this.Close();
                            }
                            retime.Text = "남은시간 : " + (AfterWards - ThisMoment).Seconds.ToString();
                            if (!check)
                            {
                                if (check3)
                                {
                                    color.Text = "";
                                    tempThisMoment = DateTime.Now;
                                    duration = new TimeSpan(0, 0, 0, 0, 3000);
                                    tempAfterWards = tempThisMoment.Add(duration);
                                    check3 = false;
                                }

                                //System.Windows.Forms.Application.DoEvents();
                                ThisMoment = DateTime.Now;

                                time = (tempAfterWards - ThisMoment).Seconds;
                                if (time <= 0)check = true;
                            }
                            else
                            {
                                result.Text = ""; //성공실패 결과
                                time = move(points);
                                if (time >= 0)
                                {
                                    if (time > 0)
                                    {
                                        result.Text = "성공";
                                        count++;
                                    }
                                    else
                                    {
                                        result.Text = "실패";
                                        if (count > 0) count--;
                                    }
                                    countb.Text = "성공 횟수 : " + count.ToString();
                                    check = false;
                                    check3 = true;
                                    check4 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        int move(Point[] p)
        {
            if (check4)
            {
                Random rand = new Random();
                updown = rand.Next(0, 7);
            }

            if (updown == 0) //청기올려
            {
                color.Text = "청기올려";
                if (check4) ss.SpeakAsync("청기올려");
                if (p[11].Y < p[2].Y && p[7].Y > p[2].Y)
                {
                    return 1;
                }
                else if (p[7].Y > p[0].Y || p[11].Y > p[0].Y || p[7].Y < p[2].Y)
                {
                    return 0;
                }
            }
            else if (updown == 1) //백기올려
            {
                color.Text = "백기올려";
                if (check4) ss.SpeakAsync("백기올려");
                if (p[11].Y > p[2].Y && p[7].Y < p[2].Y)
                {
                    return 1;
                }
                else if (p[11].Y > p[0].Y || p[11].Y < p[2].Y || p[7].Y > p[0].Y)
                {
                    return 0;
                }
            }
            else if (updown == 2) //청기내려
            {
                color.Text = "청기내려";
                if (check4) ss.SpeakAsync("청기내려");
                if (p[11].Y > p[0].Y && p[7].Y < p[0].Y)
                {
                    return 1;
                }
                else if (p[11].Y < p[2].Y || p[7].Y < p[2].Y || p[7].Y > p[0].Y)
                {
                    return 0;
                }
            }
            else if (updown == 3) //백기내려
            {
                color.Text = "백기내려";
                if (check4) ss.SpeakAsync("백기내려");
                if (p[11].Y < p[0].Y && p[7].Y > p[0].Y)
                {
                    return 1;
                }
            }
            else if (updown == 4) //청기백기올려
            {
                color.Text = "청기올리고 백기올려";
                if (check4) ss.SpeakAsync("청기올리고 백기올려");
                if (p[11].Y < p[2].Y && p[7].Y < p[2].Y)
                {
                    return 1;
                }
            }
            else if (updown == 5) //청기백기내려
            {
                color.Text = "청기내리고 백기내려";
                if (check4) ss.SpeakAsync("청기내려고 백기내려");
                if (p[11].Y > p[0].Y && p[7].Y > p[0].Y)
                {
                    return 1;
                }
            }
            else if (updown == 6) //청기올리고 백기내려
            {
                color.Text = "청기올리고 백기내려";
                if (check4) ss.SpeakAsync("청기올리고 백기내려");
                if (p[11].Y < p[2].Y && p[7].Y > p[0].Y)
                {
                    return 1;
                }
            }
            else if (updown == 7) //청기내리고 백기올려
            {
                color.Text = "청기내리고 백기올려";
                if (check4) ss.SpeakAsync("청기내리고 백기올려");
                if (p[11].Y > p[0].Y && p[7].Y < p[2].Y)
                {
                    return 1;
                }
            }
            check4 = false;
            return -1;
        }
    }
}
