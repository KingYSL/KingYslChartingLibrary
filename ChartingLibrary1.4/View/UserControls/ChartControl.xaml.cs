using ChartingLibrary1._4.Enums;
using ChartingLibrary1._4.Models;
using ChartingLibrary1._4.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ChartingLibrary1._4.View.UserControls
{
    /// <summary>
    /// Interaction logic for ChartControl.xaml
    /// Chart Control is where axis and plot positioning and type are managed cohesively
    /// rather than seperate models
    /// </summary>
    public partial class ChartControl : UserControl
    {
        #region Visibility dependency properties ------------------------------------

        // Top panel visibility
        public static readonly DependencyProperty ShowTopPanelProperty =
            DependencyProperty.Register(nameof(ShowTopPanel), typeof(bool),
                typeof(ChartControl),
                new PropertyMetadata(false, OnShowTopPanelChanged));

        public bool ShowTopPanel
        {
            get => (bool)GetValue(ShowTopPanelProperty);
            set => SetValue(ShowTopPanelProperty, value);
        }

        private static void OnShowTopPanelChanged(DependencyObject d,
                                                  DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            bool visible = (bool)e.NewValue;
            ctrl.TopPanel.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        // Bottom panel visibility
        public static readonly DependencyProperty ShowBottomPanelProperty =
            DependencyProperty.Register(nameof(ShowBottomPanel), typeof(bool),
                typeof(ChartControl),
                new PropertyMetadata(false, OnShowBottomPanelChanged));

        public bool ShowBottomPanel
        {
            get => (bool)GetValue(ShowBottomPanelProperty);
            set => SetValue(ShowBottomPanelProperty, value);
        }

        private static void OnShowBottomPanelChanged(DependencyObject d,
                                                     DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            bool visible = (bool)e.NewValue;
            ctrl.BottomPanel.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        // Left panel visibility
        public static readonly DependencyProperty ShowLeftPanelProperty =
            DependencyProperty.Register(nameof(ShowLeftPanel), typeof(bool),
                typeof(ChartControl),
                new PropertyMetadata(false, OnShowLeftPanelChanged));

        public bool ShowLeftPanel
        {
            get => (bool)GetValue(ShowLeftPanelProperty);
            set => SetValue(ShowLeftPanelProperty, value);
        }

        private static void OnShowLeftPanelChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            bool visible = (bool)e.NewValue;
            ctrl.LeftPanel.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        // Right panel visibility
        public static readonly DependencyProperty ShowRightPanelProperty =
            DependencyProperty.Register(nameof(ShowRightPanel), typeof(bool),
                typeof(ChartControl),
                new PropertyMetadata(false, OnShowRightPanelChanged));

        public bool ShowRightPanel
        {
            get => (bool)GetValue(ShowRightPanelProperty);
            set => SetValue(ShowRightPanelProperty, value);
        }

        private static void OnShowRightPanelChanged(DependencyObject d,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            bool visible = (bool)e.NewValue;
            ctrl.RightPanel.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion
        #region Content dependency properties -------------------------------------

        // Top content
        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.Register(nameof(WAxis), typeof(AxisCanvas),
                typeof(ChartControl), new PropertyMetadata(null, OnTopContentChanged));

     

        private static void OnTopContentChanged(DependencyObject d,
                                                DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            ctrl.TopPanel.Content = e.NewValue as AxisCanvas;
        }

        // Bottom content
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register(nameof(XAxis), typeof(AxisCanvas),
                typeof(ChartControl), new PropertyMetadata(null, OnBottomContentChanged));

       
        private static void OnBottomContentChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            ctrl.BottomPanel.Content = e.NewValue as AxisCanvas;
        }

        // Left content
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.Register(nameof(ZAxis), typeof(AxisCanvas),
                typeof(ChartControl), new PropertyMetadata(null, OnLeftContentChanged));


        private static void OnLeftContentChanged(DependencyObject d,
                                                 DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            ctrl.LeftPanel.Content = e.NewValue as AxisCanvas;
        }

        // Right content
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register(nameof(YAxis), typeof(AxisCanvas),
                typeof(ChartControl), new PropertyMetadata(null, OnRightContentChanged));

      

        private static void OnRightContentChanged(DependencyObject d,
                                                  DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            ctrl.RightPanel.Content = e.NewValue as AxisCanvas;
        }

        // Main content – the central area
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register(nameof(ChartingArea), typeof(Plot),
                typeof(ChartControl), new PropertyMetadata(null, OnMainContentChanged));


        private static void OnMainContentChanged(DependencyObject d,
                                                 DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ChartControl)d;
            // The XAML already binds MainContentArea.Content to this DP.
            // No extra code needed here – the binding updates automatically.
        }

        #endregion

        public Plot ChartingArea 
        {
            get => (Plot)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }
        public AxisCanvas YAxis
        {
            get => (AxisCanvas)GetValue(RightContentProperty);
            set => SetValue(RightContentProperty, value);
        }
        public AxisCanvas XAxis 
        {
            get => (AxisCanvas)GetValue(BottomContentProperty);
            set => SetValue(BottomContentProperty, value);

        }
        public AxisCanvas WAxis 
        {
            get => (AxisCanvas)GetValue(TopContentProperty);
            set => SetValue(TopContentProperty, value);
        }
        public AxisCanvas ZAxis 
        {
            get => (AxisCanvas)GetValue(LeftContentProperty);
            set => SetValue(LeftContentProperty, value);
        }
        public ZoomMode ZoomMode { get; set; }
        public ICommand ZoomChartCommand { get; set; }

        public ChartControl()
        {
            ChartingArea= new Plot();
          
         
            ChartingArea.Background= new SolidColorBrush(Colors.Red);

            ZoomChartCommand = new RelayCommand(o =>
            {
                var e = (MouseWheelEventArgs)o;  
                ZoomAxis(e);
            });

            


            InitializeComponent();
        }

        /// <summary>
        /// Simple helper that replaces the content of one region.
        /// The region is identified by an enum; the caller can also toggle visibility if desired.
        /// </summary>
        public void SwapRegion(PanelLocation location, AxisCanvas newContent)
        {
            switch (location)
            {
                case PanelLocation.Top:
                    WAxis = newContent;
                    ShowTopPanel = true;   // show it automatically
                    break;

                case PanelLocation.Bottom:
                    XAxis = newContent;
                    ShowBottomPanel = true;
                    break;

                case PanelLocation.Left:
                    ZAxis = newContent;
                    ShowLeftPanel = true;
                    break;

                case PanelLocation.Right:
                    YAxis = newContent;
                    ShowRightPanel = true;
                    break;

            }
        }

        /// <summary>
        /// Hide a region without changing its content.
        /// </summary>
        public void HideRegion(PanelLocation location)
        {
            switch (location)
            {
                case PanelLocation.Top: ShowTopPanel = false; break;
                case PanelLocation.Bottom: ShowBottomPanel = false; break;
                case PanelLocation.Left: ShowLeftPanel = false; break;
                case PanelLocation.Right: ShowRightPanel = false; break;
                    // Main has no “visibility” flag – it always occupies the center.
            }
        }
        /// <summary>
        /// Show or hide all four side panels in one call.
        /// </summary>
        public void SetPanelsVisibility(bool top, bool bottom, bool left, bool right)
        {
            ShowTopPanel = top;
            ShowBottomPanel = bottom;
            ShowLeftPanel = left;
            ShowRightPanel = right;

        }
        public void ZoomAxis(MouseWheelEventArgs e)
        {
           switch(ZoomMode)
            {
                case Enums.ZoomMode.XAxis:
                    XAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.YAxis:
                    YAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.ZAxis:
                    ZAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.WAxis:
                    WAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.Rectangulate:
                    XAxis?.OnMouseWheel(this, e);
                    YAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.Quadrate:
                    XAxis?.OnMouseWheel(this, e);
                    YAxis?.OnMouseWheel(this, e);
                    ZAxis?.OnMouseWheel(this, e);
                    WAxis?.OnMouseWheel(this, e);
                    break;
                case Enums.ZoomMode.Triangulate:
                    WAxis?.OnMouseWheel(this, e);
                    ZAxis?.OnMouseWheel(this, e);
                    YAxis?.OnMouseWheel(this, e);
                    break;
            }

        }

        public void SetYAxis()
        { YAxis = new AxisCanvas(new NumericScale(), Enums.AxisPosition.Right); }
        public void SetXAxis()
        { XAxis = new AxisCanvas(new DateTimeScale(), Enums.AxisPosition.Bottom); }
        public void SetWAxis()
        { WAxis = new AxisCanvas( new DateTimeScale(), Enums.AxisPosition.Top);
           // WAxis.Scale.Reverse = true;
        }
        public void SetZAxis()
        { ZAxis = new AxisCanvas(new NumericScale(), Enums.AxisPosition.Left);
          //  ZAxis.Scale.Reverse = true;
        }

        public void SetAxes()
        {
            SetYAxis();
            SetZAxis();
            SetXAxis();
            SetWAxis();
            
        }

    }
    public enum PanelLocation { Top, Bottom, Left, Right, Main }
}
