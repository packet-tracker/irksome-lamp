using System;

using Xamarin.Forms;
using System.Threading.Tasks;


namespace SKTest
{
    public class ListTypeTitleBar : AbsoluteLayout
    {
        public static bool mbBTConnected = false;
        public Image mImgBackground;
        public Label mLbTitle;
        public Image mImgTitle; // text 혹은 image 가 title 이 됨. 현재는 하나밖에 없으므로...
        public Image mImgBluetooth; 

        Image mImgMenu;
        Image mImgBack;

        public Label mLbExtBtn;
        public Image mImgExtBtn;    // 이미지로 되어있는 것들도 있음
        BoxView mBvCoverBack;
        //BoxView mBvExpand;

        public const float TITLE_BAR_HEIGHT_AS_FLOAT = 195.0F + 24.0F;
        public const int TITLE_BAR_HEIGHT_AS_INT = (int)TITLE_BAR_HEIGHT_AS_FLOAT;

        public static Image mImgBTOn = new Image{
            Source = ImageSource.FromResource ("SKTest.Resource.image.icon_bluetooth_on.png"),
            Aspect = Aspect.AspectFill
        };
        public static Image mImgBTOff = new Image{
            Source = ImageSource.FromResource ("SKTest.Resource.image.icon_bluetooth_off.png"),
            Aspect = Aspect.AspectFill
        };

        public ListTypeTitleBar(string title)
        {

            //Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            //Padding = new Thickness(0, Device.OnPlatform(30 * App.mfHRatio2560, 45* App.mfHRatio2560, 0), 0, 0);
            Padding = new Thickness(0, LayoutParam.PADDING_TOP * App.mfHRatio2560, 0, 0);

            mImgBackground = new Image {
                Source = ImageSource.FromResource ("SKMZGeneral.Resource.image.title_bar.png"),
                Aspect = Aspect.Fill
            };

            if (title == null) {

                mImgTitle = new Image {
                    Source = ImageSource.FromResource ("SKMZGeneral.Resource.image.skeeper_logo.png"),
                    Aspect = Aspect.Fill
                };

            } else {
                mLbTitle = new Label () {
                    Text = title,
                    //TextColor = Color.FromUint(0xFF434343), 
                    TextColor = Color.White, 
                    FontSize = 10.0 * LayoutParam.FONTSIZE_ADJUST_RATIO, 
                    FontAttributes = FontAttributes.Bold,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center
                };
            }

            mImgMenu = new Image{ 
                Source = ImageSource.FromResource("SKTest.Resource.image.icon_menu.png"),
                Aspect = Aspect.AspectFill
            };
            // menu image 의 cove 영역이 좁아서... 
            BoxView bvCoverMenu = new BoxView()
            {
                BackgroundColor = Color.Transparent
            };

            //메뉴 버튼 처리를 한다.
            var Tap_ = new TapGestureRecognizer();

            //          Tap_.Tapped += (sender, e) => {
            //              App.ShowMasterPage(true);
            //          };

            Tap_.Tapped +=  async (sender, e) =>
            {
                if (mImgMenu.IsVisible == false) return;
                mImgMenu.Opacity = .5;
                await Task.Delay(200);
                mImgMenu.Opacity = 1;
                App.ShowMasterPage(true);
            };

            bvCoverMenu.GestureRecognizers.Add(Tap_);


            mImgBack = new Image{ 
                //Source = ImageSource.FromResource("SKMZGeneral.Resource.oimage.btn_back.png"),
                Source = ImageSource.FromResource("SKTest.Resource.image.btn_prev_white.png"),
                Aspect = Aspect.AspectFill,
                IsVisible = false
            };
            mBvCoverBack = new BoxView()
            {
                BackgroundColor = Color.Transparent,
                //BackgroundColor = Color.White,
                IsVisible = false
            };

            Tap_ = new TapGestureRecognizer();
            Tap_.Tapped += async (s, e) => {
                mImgBack.Opacity = .5;
                await Task.Delay(200);
                mImgBack.Opacity = 1;
            };
            mBvCoverBack.GestureRecognizers.Add(Tap_);

            // *. bluetooth on/off
            mImgBluetooth = new Image () {
                Aspect = Aspect.AspectFill,
                IsVisible = false// 기본이 안보이는것임
            };
            if (mbBTConnected == false) {                
                mImgBluetooth.Source = mImgBTOff.Source;
            } else {                
                mImgBluetooth.Source = mImgBTOn.Source;
            }


            // *. 확장 버튼
            mLbExtBtn = new Label() {
                TextColor = Color.White,
                FontSize = 9.0 * LayoutParam.FONTSIZE_ADJUST_RATIO, 
                IsVisible = false,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };

            mImgExtBtn = new Image() {
                Aspect = Aspect.Fill,
                IsVisible = false           
            };

            //this.Children.Add(img_back, App.ConvertImageRectangle(0, 0, 1440, 239));
            this.Children.Add(mImgBackground, App.ConvertImageRectangle(0, -LayoutParam.PADDING_TOP, 1440, TITLE_BAR_HEIGHT_AS_INT + LayoutParam.PADDING_TOP));
            if (title == null) {
                this.Children.Add (mImgTitle, App.ConvertImageRectangle ((1440 - 600)/2, 16, 600, 195));
            } else {
                this.Children.Add (mLbTitle, App.ConvertImageRectangle (0, 16, 1440, TITLE_BAR_HEIGHT_AS_INT - 16));
            }
            this.Children.Add(mImgMenu, App.ConvertImageRectangle(14, 18+24, 160, 158));
            this.Children.Add(bvCoverMenu, App.ConvertImageRectangle(0,0, TITLE_BAR_HEIGHT_AS_INT + 80, TITLE_BAR_HEIGHT_AS_INT));  // 조금 넓혀줌
            this.Children.Add(mImgBack, App.ConvertImageRectangle(14, 18+24, 160, 158));
            this.Children.Add(mBvCoverBack, App.ConvertImageRectangle(0, 0, TITLE_BAR_HEIGHT_AS_INT + 80, TITLE_BAR_HEIGHT_AS_INT));    // 조금 넓혀줌
            this.Children.Add(mImgBluetooth, App.ConvertImageRectangle(1315, (TITLE_BAR_HEIGHT_AS_INT - 89) / 2, 49, 89));
            this.Children.Add(mLbExtBtn, App.ConvertImageRectangle(1200, 16, 240, TITLE_BAR_HEIGHT_AS_INT - 16));
            this.Children.Add(mImgExtBtn, App.ConvertImageRectangle(1200, 16, 240, TITLE_BAR_HEIGHT_AS_INT - 16));
            //this.Children.Add(vHorizLine, App.ConvertImageRectangle(0, 194, -1, 16));

        }

        // 오른쪽에 확장 버튼이 있음. 블루투스 아이콘을 가리고, 이넘을 보여주는것으로 함
        // ==> bluetooth 아이콘은 home/청진 화면에서만 나타남..
        public void SetExtendButton(string sButton, EventHandler eh)
        {
            if (sButton == null) {              
                mLbExtBtn.IsVisible = false;
                return;
            }
            mLbExtBtn.Text = sButton;
            mLbExtBtn.IsVisible = true;


            if (eh != null) {
                mLbExtBtn.GestureRecognizers.Clear ();
                var Tap_ = new TapGestureRecognizer ();
                Tap_.Tapped += eh;
                mLbExtBtn.GestureRecognizers.Add (Tap_);
            }

        }

        // image 로 된 것일 때
        public void SetExtendButton(ImageSource isButton, EventHandler eh)
        {

            if (isButton == null) {
                mImgExtBtn.IsVisible = false;
                return;
            }
            mImgExtBtn.Source = isButton;
            mImgExtBtn.IsVisible = true;

            if (eh != null) {
                mImgExtBtn.GestureRecognizers.Clear ();
                var Tap_ = new TapGestureRecognizer ();
                Tap_.Tapped += eh;
                mImgExtBtn.GestureRecognizers.Add (Tap_);
            }
        }

        public void SetBackButton(EventHandler eh)
        {
            mImgBack.IsVisible = true;
            mImgMenu.IsVisible = false;
            mBvCoverBack.IsVisible = true;

            if (mBvCoverBack.GestureRecognizers.Count > 1) {
                mBvCoverBack.GestureRecognizers.RemoveAt (mBvCoverBack.GestureRecognizers.Count - 1);
            }
            var Tap_ = new TapGestureRecognizer();
            Tap_.Tapped += eh;
            mBvCoverBack.GestureRecognizers.Add(Tap_);
        }

        public void RestoreMenuButton()
        {
            mImgMenu.IsVisible = true;
            mImgBack.IsVisible = false;
            mBvCoverBack.IsVisible = false;

            if (mBvCoverBack.GestureRecognizers.Count > 0) {
                mBvCoverBack.GestureRecognizers.RemoveAt (mBvCoverBack.GestureRecognizers.Count - 1);
            }
        }


        public void SetStartMode(bool bStart = true)
        {
            mImgMenu.IsVisible = !bStart;
        }

    }
}


