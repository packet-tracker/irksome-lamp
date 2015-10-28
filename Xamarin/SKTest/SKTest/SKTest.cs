using System;

using Xamarin.Forms;
using System.Diagnostics;
//using XLabs.Ioc;
//using XLabs.Platform.Device;
using System.Reflection;
using System.Collections.Generic;


namespace SKTest
{
    public interface ISKTestViewStart
    {
        /* 모든 view에 공통적으로 적용되는 startup 함수. Menu로 부터 새로 호출할 때 수행 */
        bool StartView();
    }

//    //public class RootPage : CustomMasterDetailPage
//    public class RootPage : MasterDetailPage
//    {
//        public RootPage ()
//        {
//            Master = new MenuPage ();
//            Detail = null;//new home();
//            //Master.WidthRequest = 1440;   // not work
//            //DrawerWidth = ;
//            //DrawerWidth = (int)(2100 * App.mfWRatio1440);
//            //MasterBehavior = MasterBehavior.SplitOnPortrait;
//            //MasterBehavior = MasterBehavior.Popover;
//        }
//        public RootPage(ContentPage DetailPage)
//        {
//            Master = new MenuPage();
//            Detail = DetailPage;
//            //Master.WidthRequest = 1440;
//            //DrawerWidth = -1;
//            //DrawerWidth = (int)(2100 * App.mfWRatio1440);
//            //MasterBehavior = MasterBehavior.SplitOnPortrait;
//            //MasterBehavior = MasterBehavior.Popover;
//        }
//    }


    // *. 타이틀바가 있는 화면의 기본 base page. 모든 page 가 이것을 부모로 설정 
    public class SKTestContentPage : ContentPage
    {

        public ListTypeTitleBar mTitleBar;

        public SKTestContentPage() : base()
        {
        }


    }

    public class App : Application
    {
        #if _XXX_
        AlertDialog mDialogMozartDisconnected = null;
        ProgressDialog mMozartProgressDialog = null;
        #endif

        public static bool mbIOS = false;

        // *. Dialog box 관련
        public const int DIALOG_DATA_PREPARE_PROGRESS = 4;
        public const int DIALOG_MOZART_DISCONNECTED = 5;

        public static System.Threading.Tasks.Task mAlertDlgTask = null; 

        //public static ListTypeTitleBar mListTitleBar;
        public static Image mImgBluetoothOnOff;

        public enum enumLZ{ // LZ : LocaliZation 
            LI_KOREA,
            LI_ENGLISH,
            LI_CHINA_RCN,
            LI_CHINA_RTW,
            LI_JAPAN
        };

        public static enumLZ mCurrLZ;
        public static int mnOSVer;  // 


        //Button mTest;

        // device 기준 높이/너비
        public static double mdWidth;
        public static double mdHeight;

        public static double mdDisplayWidth;
        public static double mdDisplayHeight;

        public static float mfWRatio1440;       // 이번부터는 가로 기준이 mfWRatio1440
        public static float mfHRatio2560;       // 세로 기준은 2560, 아래 값

        //public const float mfBaseHeight = 2560.0F;    // 2560 이 아닐 경우가 있음 : 안드로이드 폰 맨 위의 영역 안나올 때 (Kitkat 이하)
        public const int mnBaseHeight = 2560;   

        // *. 기본 view 들. SetThisViewAs 에도 등록 필요
        public static HomeScreenView mHomeScreenView;
//        public static MeasureView mMeasureView;
//        public static PlayView mPlayView;   // 미리 만들어줌
        #if __TODO_201511__
        public static ProfileView mProfileView;
        #endif
//        public static ProfileAMView mProfileAMView;
//        public static HistoryView mHistoryView;
//        public static SettingView mSettingView;
//        public static GuideView mGuideView;

        public static List<ContentPage> mViews = new List<ContentPage>();   
        /// 메뉴에서 선택하는 것만 따로 관리 (playview, profileamview 등은 아님)

        public static Image mImgNoProfile;  // 많이 사용하므로...
        public static ImageSource mImgSrcBar;

//        public static AlertView mAlertDlg;



        public App ()
        {
            // The root page of your application
            //MainPage = GetMainPage();
            //return;
            #if DEBUG           
            System.Diagnostics.Debug.WriteLine("===============");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames()) 
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            #endif


            //if (Device.OS != TargetPlatform.WinPhone)
//            SKTestStrings.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
//            //}
//
//
//            var device = Resolver.Resolve<IDevice>();
//
//            // 아래 값은 iphone, ipad 에 따라 달라지고, 실제 기기 해상도에 따라서는 달라지지 않음
//            // 보통 좌표 배치는 이 값을 기준으로 함            
//            mdWidth = device.WidthRequestInInches(device.ScreenWidthInches());  // 320
//            mdHeight = device.HeightRequestInInches(device.ScreenHeightInches());   // 640
//            // iPhone 4s 의 경우 640 x 960
//            // iPhone 5 의 경우 320 x 568
//            // 가로형 pad 중 huawei mediapad 10 link+ : 1232 x 800. 
//            // 이넘의 경우 기본이 가로(landscape 화면이며, 가로가 더 넓음. 이를 바꾸어 주어야 함
//
//
//
//            mdDisplayWidth = device.Display.Width;
//            mdDisplayHeight = device.Display.Height;
//            // iPhone 5 의 경우 640 * 1136
//            // 가로형 pad 중 huawei mediapad 10 link+ : 1232 x 800. 위와 같음
//
//            //          if (LayoutParam.PADDING_TOP == 0) {
//            //              mfBaseHeight = (float)(2560);
//            //              mnBaseHeight = 2560;
//            //
//            //          } else {
//            //              mfBaseHeight = 2560.0F;
//            //              mnBaseHeight = 2560;
//            //          }


//            if (device.IsInLandscape()) {
//                double dTemp = mdWidth;
//                mdWidth = mdHeight;
//                mdHeight = dTemp;
//
//                dTemp = mdDisplayWidth;
//                mdDisplayWidth = mdDisplayHeight;
//                mdDisplayHeight = dTemp;
//            }

            //          if (App.mbIOS) {
            mfWRatio1440 = (float)(mdWidth / 1440);
            mfHRatio2560 = (float)mdHeight / (float)mnBaseHeight;   
            //          } else {
            //              mfWRatio1440 = (float)(mdDisplayWidth / 1440);
            //              mfHRatio2560 = (float)(mdDisplayHeight / 2560); 
            //          }


            if (App.mbIOS) {
                //LayoutParam.FONTSIZE_ADJUST_RATIO = 3.8;
                LayoutParam.FONTSIZE_ADJUST_RATIO = 7.8 * mfHRatio2560;
            } else {

                //LayoutParam.FONTSIZE_ADJUST_RATIO = 8.0 * mfHRatio2560;
                LayoutParam.FONTSIZE_ADJUST_RATIO = 7.2 * mfHRatio2560;
            }


            InitializeApp(); 

            //MainPage = new XLabs.Sample.Pages.Controls.GestureSample ();
//            if (SettingInfo.mCurrSettings.showintro) {
//                // 아래에 몇몇 view 들을 미리 읽어놓는데, intro 가 나가는 동안에 읽으면 도움이 될까 싶어...
//                // 크게 도움은 안되는듯
//                MainPage = new IntroView ();
//            }

            // *. 많이 사용되는 image 들 미리 loading
            mImgNoProfile = new Image{
                Source = ImageSource.FromResource("SKMETest.Resource.image.icon_profile.png")
            };

            mImgSrcBar = ImageSource.FromResource ("SKMETest.Resource.image.icon_bar.png");

//            mMeasureView = new MeasureView();
//            mPlayView = new PlayView();
//            #if __TODO_201511__
//            mProfileView = new ProfileView ();  // 당분간 profile 은 1 app 1 profile
//            #endif
//            mHistoryView = new HistoryView ();
//            mProfileAMView = new ProfileAMView(null);
//            mViews.Add(mMeasureView);
//            #if __TODO_201511__
//            mViews.Add(mProfileView);
//            #else
//            mViews.Add(mProfileAMView);
//            #endif
//
//
//            mViews.Add(mHistoryView);
//
//            if (SettingInfo.mCurrSettings.showintro == false) {
//                EndIntro ();
//            }
//
//
//
//            mAlertDlg = new AlertView ();
//
//            //          var page = new NavigationPage();
//            //
//            //          var introview = new IntroView ();
//            //
//            //
//            //          NavigationPage.SetHasNavigationBar (introview, false);
//            //          NavigationPage.SetHasNavigationBar (mProfileView, false);
//            //          //NavigationPage.SetHasBackButton (introview, false);
//            //          page.PushAsync (mProfileView);
//            //          page.PushAsync (introview);
//            //
//            //          MainPage = page;
//
//
//
//
//
//
//            //mPlayView.StartPlayView(70, 90, -27, false);
//            //MainPage = new RootPage (mPlayView);//mMeasureView;
//
//
//
//            //
//            //          Button mTest = new Button {
//            //              Text = "Bluetooth start"
//            //          };
//            //
//            //
//            //          mTest.Clicked += OnTestButtonClicked;
//            //
//            //          MainPage = new RootPage(new ContentPage {
//            //              Content = new StackLayout {
//            //                  VerticalOptions = LayoutOptions.Center,
//            //                  Children = {
//            //                      new Label {
//            //                          XAlign = TextAlignment.Center,
//            //                          Text = "Android/iOS bluetooth test!"
//            //                      },
//            //                      mTest
//            //                  }
//            //              }
//            //          });
        }


        void InitializeApp()
        {
            // *. 세팅값 읽기
//            SettingDatabase.LoadSettings();
//            GetUserPfofiles ();

            // *. Localization 설정
            //string sLang = new ILocalize().GetCurrentCultureInfo().TwoLetterISOLanguageName;
            // en, ko, ja, zh, 

            string sLang = DependencyService.Get<ILocalize> ().GetCurrentCultureInfo().Name;
            // ko-KR, en-US, zh-CN, zh-TW, ja-JP, 첫번재는 언어, 두번째는 지역. iOS 의 경우 지역이 있음(android는?? 모름)
            // https://technet.microsoft.com/ko-kr/library/Dd744369(v=WS.10).aspx

            if (sLang.Contains ("ko"))
                App.mCurrLZ = enumLZ.LI_KOREA;
            else if (sLang.Contains ("CN")) 
                App.mCurrLZ = enumLZ.LI_CHINA_RCN;
            else if (sLang.Contains ("TW")) 
                App.mCurrLZ = enumLZ.LI_CHINA_RTW;
            else if (sLang.Contains ("ja")) 
                App.mCurrLZ = enumLZ.LI_JAPAN;
            else 
                App.mCurrLZ = enumLZ.LI_ENGLISH;

        }






        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }

        public static void Main<T>()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {

                App.Current.MainPage = (Page)Activator.CreateInstance (typeof(T));
                GC.Collect ();

            });
        }


//        public static void EndIntro()
//        {
//
//            //          var p = ((NavigationPage)App.Current.MainPage).PopAsync ();
//            //          //((ContentPage)p.Result).Content = null;
//            //          return;
//
//            //App.Current.MainPage = new RootPage (new MediQnAView());
//            //return;
//
//
//            //*. 처음 화면
//            #if __TODO_201511__
//            if (ProfileInfo.mListProfiles.Count > 0) {
//            if (ProfileInfo.mPfCurr != null) {                  
//            mSelMModeView = new SelMModeView ();
//            App.Current.MainPage = new RootPage (mSelMModeView);
//            mSelMModeView.StartView();
//            App.mViews.Add (mSelMModeView);
//            }
//            else {
//            // profile select
//            mProfileView = new ProfileView();
//            App.Current.MainPage = new RootPage (mProfileView);             
//            mProfileView.StartView();
//            App.mViews.Add (mProfileView);
//
//            }
//            }
//            else {
//            // 바로 add 진행함               
//            //mProfileAMView.SetProfile(null);
//            App.Current.MainPage = new RootPage (mProfileAMView);
//            mImgBluetoothOnOff = mProfileAMView.mTitleBar.mImgBluetooth;
//            //mProfileAMView.StartView();
//            //App.mViews.Add (mProfileAMView);
//            }
//            #else
//
//
//
//            if (ProfileInfo.mListProfiles.Count > 0 && ProfileInfo.mPfCurr != null) {
//
//                mHomeScreenView = new HomeScreenView ();
//                App.Current.MainPage = new RootPage (mHomeScreenView);
//                mHomeScreenView.StartView();
//                App.mViews.Add (mHomeScreenView);
//
//            }
//            else {
//                // 바로 add 진행함               
//                //mProfileAMView.SetProfile(null);
//                App.Current.MainPage = new RootPage (mProfileAMView);
//                mImgBluetoothOnOff = mProfileAMView.mTitleBar.mImgBluetooth;
//                mProfileAMView.SetProfile(null);
//
//
//
//
//                //mProfileAMView.StartView();
//                //App.mViews.Add (mProfileAMView);
//            }
//
//            #endif
//        }


        public static Page GetMainPage ()
        {   


            //return new OpenGLPage ();


            var myLabel = new Label () {
                //Text = "Hello World",
                //Font = Font.SystemFontOfSize (20),
                FontSize = Font.SystemFontOfSize(20).FontSize,
                TextColor = Color.Red,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };


            //          #region Display information
            //          var display = device.Display;
            //
            //          var displayFrame = new Frame();
            //          if (display != null)
            //          {
            //              displayFrame.Content = new StackLayout()
            //              {
            //                  Children =
            //                  {
            //                      new Label() { Text = display.ToString() },
            //                      new Label() { Text = string.Format("Screen width is\t {0:0.0} inches.", display.ScreenWidthInches()) },
            //                      new Label() { Text = string.Format("Screen height is\t {0:0.0} inches.", display.ScreenHeightInches()) },
            //                      new Label() { Text = string.Format("Screen diagonal size is\t {0:0.0} inches.", display.ScreenSizeInches()) }
            //                  }
            //                  };
            //          }
            //          else
            //          {
            //              displayFrame.Content = new Label() { TextColor = Color.Red, Text = "Device does not contain display information." };
            //          }
            //
            //          stack.Children.Add(displayFrame); 
            //          #endregion

            //var display = device.Display;


            // IPhone6 : 375,667
            // Galaxy 4 :1080, 1920

            var myImage = new Image () {
                Source = FileImageSource.FromFile("bg_intro.png")   // 각 platform 별 resource 디렉토리에서...

            };


            RelativeLayout layout = new RelativeLayout (){
                BackgroundColor = Color.Black
            };

            //myLabel.Text = String.Format ("Width:{0} Height:{1}\nLayout Width:{2} Layout Height:{3}", 
            //  mnScreenWidth, mnScreenHeight, layout.Width, layout.Height);

            myLabel.Text = String.Format ("Width:{0} Height:{1}\nLayout Width:{2} Layout Height:{3}", 
                mdWidth, mdHeight, layout.Width, layout.Height);


            layout.Children.Add (myImage, 
                Constraint.Constant (0), 
                Constraint.Constant (0),
                Constraint.RelativeToParent ((parent) => { return parent.Width; }),
                Constraint.RelativeToParent ((parent) => { return parent.Height; }));

            layout.Children.Add (myLabel, 
                Constraint.Constant (0), 
                Constraint.Constant (0),
                Constraint.RelativeToParent ((parent) => { return parent.Width; }),
                Constraint.RelativeToParent ((parent) => { return parent.Height; }));

            return new ContentPage { 
                Content = layout
            };
        }


        public static View mpPrev;
        public static void ShowDialog(int id) {
            switch(id) {    

            case DIALOG_DATA_PREPARE_PROGRESS: {
                    #if _XXX_
                    if(mMozartProgressDialog.isShowing() == false)
                    mMozartProgressDialog.show();
                    #endif
                }
                break;

//            case DIALOG_MOZART_DISCONNECTED: {
//
//                    //                  //if (mAlertDlgTask == null) {
//                    //                      mAlertDlgTask = MainPage.DisplayAlert ("SKeeper Mozart", "Mozart disconnectd", "OK");
//                    //                  //}
//
//                    if (App.Current.MainPage.GetType() == typeof(IntroView)) return;
//
//                    if (App.mAlertDlg.IsVisible)
//                        return;
//
//                    var root = (MasterDetailPage)App.Current.MainPage;
//                    App.mAlertDlg.SetKind (AlertView.DIALOG_BLUETOOTH_ALERT);
//                    App.mAlertDlg.IsVisible = true;
//
//                    //                  View pContent = ((Layout<View>)((ContentPage)root.Detail).Content);
//                    Layout<View> vPrev = (Layout<View>)((ContentPage)root.Detail).Content;
//                    //Debug.WriteLine("type:" + vPrev.GetType());
//                    if (vPrev.GetType () == typeof(AbsoluteLayout)) {
//
//                        vPrev.Children.Add(App.mAlertDlg);
//                    } else {
//                        // 우선 이렇게라도 하면 되긴 되는데, 배경이 반투명으로 나오지 않음
//                        // 나온다 해도 아마도 아래로 밀린 상태일것임
//                        //vPrev.Children.Insert(0,App.mAlertDlg);
//
//                        // 급하게 al 을 추가해봄. height 만 알아도... 이렇게 안해도 되는데..// view 를 새로 달아 주는것이라서, 만약 스크롤 되어 있는 상황이면 스크롤이 취소됨
//                        AbsoluteLayout al = new AbsoluteLayout();
//
//                        al.Children.Add (vPrev);
//                        al.Children.Add(App.mAlertDlg);
//                        ((ContentPage)root.Detail).Content = al;
//                        mpPrev = vPrev;
//                    }
//                    // 나름 modal view 이기 때문에, root.detail 이 바뀌는 일은 없다 판단함
//
//                }
//                break;
            }
        }

        public static void HideDialog(int id) {

            switch(id) {
            case DIALOG_DATA_PREPARE_PROGRESS: {

                }
                break;
//            case DIALOG_MOZART_DISCONNECTED: {
//                    //                  if(mAlertDlgTask == null)
//                    //                      return;
//                    //
//                    //                  if (mAlertDlgTask.Status == System.Threading.Tasks.TaskStatus.Running) {                        
//                    //                      //mAlertDlgTask.Dispose ();
//                    //                  }
//                    if (App.Current.MainPage.GetType() == typeof(IntroView)) return;
//
//                    if (App.mAlertDlg.IsVisible) {
//                        var root = (MasterDetailPage)App.Current.MainPage;
//
//                        if (mpPrev != null) {
//
//                            Layout<View> vPrev = (Layout<View>)((ContentPage)root.Detail).Content;
//                            vPrev.Children.RemoveAt (0);
//                            ((ContentPage)root.Detail).Content = mpPrev;
//                            mpPrev = null;
//                        } else {
//
//                            ((Layout<View>)((ContentPage)root.Detail).Content).Children.Remove (App.mAlertDlg);
//                        }
//                        //((Layout<View>) ((ContentPage)root.Detail).Content).Children.RemoveAt(0);
//                        App.mAlertDlg.IsVisible = false;
//
//                    }
//
//                }
//                break;
            }

            #if _XXX_
            switch(id) {
            case DIALOG_DATA_PREPARE_PROGRESS: {
            if(mMozartProgressDialog == null)
            return;

            if(mMozartProgressDialog.isShowing())
            mMozartProgressDialog.dismiss();
            }
            break;
            case DIALOG_MOZART_DISCONNECTED: {
            if(mDialogMozartDisconnected == null)
            return;

            if(mDialogMozartDisconnected.isShowing())
            mDialogMozartDisconnected.dismiss();
            }
            break;
            }
            #endif
        }


        public static Rectangle ConvertImageRectangle(int x, int y, int width, int height)
        {
            Rectangle tmp = new Rectangle();
            //  tmp.X = x * devicewidth / PhotoShopImageSize_X;
            //  if (Device.OS == TargetPlatform.Android)
            //  {
            //      //25~48dp 평균 타이틀바 높이
            //      //640 dpi = 40
            //      //1290 dpi = 56
            //      tmp.Y = y * (deviceheight-40) / PhotoShopImageSize_Y;
            //  }
            //  else
            //  {   //아이폰은 테스트 필요
            //      tmp.Y = y * (deviceheight-20) / PhotoShopImageSize_Y;
            //  }
            //  tmp.Width = width * devicewidth / PhotoShopImageSize_X;
            //  tmp.Height = height * deviceheight / PhotoShopImageSize_Y;

            // title bar 가 있고 없고는 사실 그리 크게 영향을 끼치지는 않음. 없는것으로 무조건 진행하고,
            // 이 함수로 모든 비율 계산 대체. 아래 ConvertImageRectangleNoTitleBarType 함수는 사용하지 않음
            tmp.X = (float) x * mfWRatio1440;
            tmp.Y = (float) y * mfHRatio2560;
            if (width > 0)
                tmp.Width = (float)width * mfWRatio1440;
            else
                tmp.Width = mdWidth;

            if (height > 0)
                tmp.Height = (float)height * mfHRatio2560;
            else
                tmp.Height = mdHeight;

            return tmp;
        }


        public static Rectangle ConvertImageRectangleFR(int x, int y, int width, int height)
        {
            // FR : Fixed ratio. 어떤 그림들은 세로 기준으로 크기를 같은 비율로 바꾸어줌. 그렇게 해야 모양이 안깨짐(pad 같은 경우) 

            Rectangle tmp = new Rectangle();
            tmp.X = (float) x * mfWRatio1440;
            tmp.Y = (float) y * mfHRatio2560;
            tmp.Width = (float) width * mfHRatio2560;   // 이부분만 달라짐
            tmp.Height = (float) height * mfHRatio2560;

            return tmp;
        }



        public static Rectangle ConvertDisplayRectangle(int x, int y, int width, int height)
        {
            Rectangle tmp = new Rectangle();
            tmp.X = x * mdDisplayWidth * (1.0/1440.0);
            if (Device.OS == TargetPlatform.Android)
            {
                //25~48dp 평균 타이틀바 높이
                //640 dpi = 40
                //1290 dpi = 56
                tmp.Y = y * (mdDisplayHeight-40) * (1.0/(double)App.mnBaseHeight);
            }
            else
            {   //아이폰은 테스트 필요
                tmp.Y = y * (mdDisplayHeight-20) * (1.0/(double)App.mnBaseHeight);
            }
            tmp.Width = width * mdDisplayWidth  * (1.0/1440.0);
            tmp.Height = height * mdDisplayHeight * (1.0/(double)App.mnBaseHeight);
            return tmp;
        }


        public static void ShowMasterPage(bool show)
        {

            var root = (MasterDetailPage)App.Current.MainPage;
            root.IsPresented = show;
        }

        public static void SetThisViewAs(ContentPage cp, Type type)
        {
            // Menu 에서 호출되는 것들만... cp 가 mViews 에 없음은 이미 확인되었음
            // 정녕 C# 에는 이것을 loop 를 돌려서 해결할 방법이 없단 말인가... 흠..
            if (type == typeof(HomeScreenView)) {
                mHomeScreenView = (HomeScreenView)cp;
                mViews.Add (cp);
            }


        }



    }
}

