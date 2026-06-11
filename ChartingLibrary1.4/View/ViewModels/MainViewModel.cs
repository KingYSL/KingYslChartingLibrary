using ChartingLibrary1._4.Enums;
using ChartingLibrary1._4.Models;
using ChartingLibrary1._4.Utilities;
using ChartingLibrary1._4.View.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChartingLibrary1._4.View.ViewModels
{
    public class MainViewModel : ObservableObject
    {
       
        public bool Loaded = false;
        public ChartControl PlotControl { get; set; }
       
        public MainViewModel()
        {

           PlotControl = new ChartControl();

            PlotControl.SetPanelsVisibility(true, true, true, true);
            PlotControl.SetAxes();
            RunAllTestsAsync();
         
        }

        public async Task Hide()
        {
            await Task.Delay(2000);
            PlotControl.HideRegion(PanelLocation.Right);
            Console.Beep();
            await Task.Delay(2000);
            PlotControl.SetPanelsVisibility(false, false, false, true);


        }
        private async Task RunAllTestsAsync()
        {
            try
            {
                await CascadeShowAsync();
                await CascadeHideAsync();
                await BlinkAllAsync(3);
                await SwapAxisAtRuntimeAsync();
                await RandomToggleAsync(8);
                await ShowOppositePairsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Test run failed: {ex}");
            }
        }

        // Panels appear one at a time, 500ms apart
        public async Task CascadeShowAsync()
        {
            foreach (var loc in new[] { PanelLocation.Top, PanelLocation.Right,
                                    PanelLocation.Bottom, PanelLocation.Left })
            {
                SetVisibility(loc, true);
                await Task.Delay(500);
            }
            Console.Beep();
        }

        // Panels disappear one at a time
        public async Task CascadeHideAsync()
        {
            foreach (var loc in new[] { PanelLocation.Left, PanelLocation.Bottom,
                                    PanelLocation.Right, PanelLocation.Top })
            {
                PlotControl.HideRegion(loc);
                await Task.Delay(500);
            }
            Console.Beep();
        }

        // All four panels blink on/off N times
        public async Task BlinkAllAsync(int times)
        {
            for (int i = 0; i < times; i++)
            {
                PlotControl.SetPanelsVisibility(true, true, true, true);
                await Task.Delay(300);
                PlotControl.SetPanelsVisibility(false, false, false, false);
                await Task.Delay(300);
            }
        }

        // Swap the Y axis for a fresh one mid-run to confirm content DP propagates
        public async Task SwapAxisAtRuntimeAsync()
        {
            PlotControl.SetPanelsVisibility(false, false, false, true);
            await Task.Delay(800);

            var replacement = new AxisCanvas(new NumericScale(), Enums.AxisPosition.Right);
            PlotControl.SwapRegion(PanelLocation.Right, replacement);
            Debug.WriteLine("Y axis replaced");
            await Task.Delay(1000);
        }

        // Flip random panels — useful for spotting layout glitches
        public async Task RandomToggleAsync(int iterations)
        {
            var rng = new Random();
            var locs = new[] { PanelLocation.Top, PanelLocation.Bottom,
                           PanelLocation.Left, PanelLocation.Right };
            for (int i = 0; i < iterations; i++)
            {
                var loc = locs[rng.Next(locs.Length)];
                bool show = rng.Next(2) == 0;
                SetVisibility(loc, show);
                await Task.Delay(250);
            }
        }

        // Top+Bottom together, then Left+Right together — checks symmetry
        public async Task ShowOppositePairsAsync()
        {
            PlotControl.SetPanelsVisibility(true, true, false, false);
            await Task.Delay(800);
            PlotControl.SetPanelsVisibility(false, false, true, true);
            await Task.Delay(800);
            PlotControl.SetPanelsVisibility(true, true, true, true);
        }

        private void SetVisibility(PanelLocation loc, bool show)
        {
            if (show)
            {
                // SwapRegion auto-shows, but content's already set — re-set to current to force show
                switch (loc)
                {
                    case PanelLocation.Top: PlotControl.ShowTopPanel = true; break;
                    case PanelLocation.Bottom: PlotControl.ShowBottomPanel = true; break;
                    case PanelLocation.Left: PlotControl.ShowLeftPanel = true; break;
                    case PanelLocation.Right: PlotControl.ShowRightPanel = true; break;
                }
            }
            else
            {
                PlotControl.HideRegion(loc);
            }
        }
    }

}

