using System;

using Xamarin.Forms;
using System.Globalization;
using System.Text;

#if __ANDROID__
using Android.Media;
using Android.OS;
#endif

namespace SKTest
{
    public class Parameters : ContentPage
    {
        public Parameters ()
        {
            Content = new StackLayout { 
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }

    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo ();
        //void SetLocale ();
    }

    public class LayoutParam {

        // *. width, height ==> App 으로 돌림
        //      public static int mnWidthPixel;
        //      public static int mnHeightPixel;
        //      /// HUAWEI 의 경우 : 800 * 1232 인데, 기본 방향이 landscape 임 
        //
        //      // *. UI 가 1080 * 1920 기준으로 되어 있으므로 이를 고려한 ratio 설정
        //      public static float mfWRatio1080;
        //      public static float mfHRatio1920;

        // *. 상단 padding
        public static int PADDING_TOP = 40;//50;    // iOS 기준


        public static double FONTSIZE_ADJUST_RATIO = 3.8;//4.5  // iOS 기준
        public static float mfDensity;

        public const int LAYOUT_BASE_WIDTH = 1080;
        public const int LAYOUT_BASE_HEIGHT = 1920;

        // *. colors,   Xamarin 으로 옮겨오면서 정의해 준 값들
        public const uint BG_COLOR_GREEN_ONE = unchecked((uint)0xFFC0DD76); // 메뉴에서의 background ==> OLD ver.
        public const uint BG_COLOR_GRBL_ONE = unchecked((uint)0xFF00959B);  // 바뀜, 청녹색 계열
        public readonly uint BG_COLOR_TITLE_HEART = BG_COLOR_GRBL_ONE;//BG_COLOR_GREEN_ONE;
        public const uint BG_COLOR_TITLE_FETAL = unchecked((uint)0xFFF6ACAB);
        public const uint BG_COLOR_TITLE_ANIMAL = unchecked((uint)0xFF87D4E4);
        // 아래부터는 new app
        //public const Color BG_COLOR_GRAY_ONE = Color.FromUint(0xFFF2F2F2);    // C# 은 안됨
        public static readonly Color COLOR_WHITE_ONE = Color.FromUint(0xFFF2F2F2);

        public static readonly Color COLOR_GENERAL_GRAY = Color.FromUint(0xFF898A8C);   
        public static readonly Color COLOR_GENERAL_GRBL = Color.FromUint(BG_COLOR_GRBL_ONE);    
        /// Text 등에서 만이 쓰는 일반적인 회색, 137/138/140



        // *. 측정 그래프
        public const int GRAPH_HEARTBEAT_WIDTH = 960;
        public const int GRAPH_HEARTBEAT_HEIGHT = 585;
        public const int GRAPH_HEARTBEAT_TIME_HEIGHT = 60;

        // size 를 줄여도 멈칫 하는 증상 있음. // 2014. 8. 13. SongKei
        //public const int GRAPH_HEARTBEAT_WIDTH = 240;
        //public const int GRAPH_HEARTBEAT_HEIGHT = 146;
        //public const int GRAPH_HEARTBEAT_TIME_HEIGHT = 15;

        public readonly int GRAPH_HEARTBEAT_TOTAL_HEIGHT = 
            (GRAPH_HEARTBEAT_HEIGHT + GRAPH_HEARTBEAT_TIME_HEIGHT);

        // *. history 그래프
        //public const int GRAPH_HISTORY_WIDTH  // 없음 (full 로 사용.mnWidthPixel 사용)   
        public const int GRAPH_HISTORY_HEIGHT = 680;    // 610 에서 680 으로 증가 // 2014. 8. 14. SongKei
        public const int GRAPH_HISTORY_DATE_HEIGHT = 76;    // 상단의 날짜 표시 영역
        public const int GRAPH_HISTORY_XAXIS_HEIGHT = 56;   // 하단의 x(시간) label

        // *. guide 그림
        //public const int GUIDE_IMAGE_WIDTH = 967;
        //public const int GUIDE_IMAGE_HEIGHT = 1107;
        public const int GUIDE_IMAGE_WIDTH = 676;
        public const int GUIDE_IMAGE_HEIGHT = 774;


        // *. 진단 결과 창
        public const int RES_ANALYSIS_HEIGHT = 1550;


        /////////////////////////////////////////////////////////
        //  Function Name : GetTitleBGColor
        //  Parameters : mode
        //  Invoked : 모드 바뀔 때 및 메뉴에서 measure 선택했을 때
        ////////////////////////////////////////////////////////// 2014. 6. 19. SongKei

        public static uint GetTitleBGColor(int mnMode) {

            uint nColor;
            nColor = BG_COLOR_GREEN_ONE;

//            switch (mnMode) {
//            case Setting.PROFILE_MODE_FETAL:
//                nColor = BG_COLOR_TITLE_FETAL;
//                break;
//            case Setting.PROFILE_MODE_ANIMAL:
//                nColor = BG_COLOR_TITLE_ANIMAL;
//                break;
//            default:
//                nColor = BG_COLOR_GREEN_ONE;
//                break;
//            }

            return nColor;
        }
    }
}


