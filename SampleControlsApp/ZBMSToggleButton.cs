using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace SampleControlsApp
{
   
    public sealed class ZBMSToggleButton : Control
    {
        private Button _button;
        private ProgressRing _progressRing;
        private TextBlock _onOffText;
        private TextBlock _header;
        private SolidColorBrush _colorBrush;

        public event  EventHandler<RoutedEventArgs> Toggled;

        public ZBMSToggleButton()
        {
            this.DefaultStyleKey = typeof(ZBMSToggleButton);
        }

        public SolidColorBrush OnOffForeground
        {
            get { return (SolidColorBrush)GetValue(OnOffForegroundProperty); }
            set { SetValue(OnOffForegroundProperty, value); }
        }

        public static readonly DependencyProperty OnOffForegroundProperty =
            DependencyProperty.Register(nameof(OnOffForeground), typeof(SolidColorBrush), typeof(ZBMSToggleButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public SolidColorBrush HeaderForeground
        {
            get { return (SolidColorBrush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register(nameof(HeaderForeground), typeof(SolidColorBrush), typeof(ZBMSToggleButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public string OnOffContent
        {
            get { return (string)GetValue(OnOffContentProperty); }
            set { SetValue(OnOffContentProperty, value); }
        }
                
        public static readonly DependencyProperty OnOffContentProperty =
            DependencyProperty.Register(nameof(OnOffContent), typeof(string), typeof(ZBMSToggleButton), new PropertyMetadata(null));


        public double OnOffFontSize
        {
            get { return (double)GetValue(OnOffFontSizeProperty); }
            set { SetValue(OnOffFontSizeProperty, value); }
        }

        public static readonly DependencyProperty OnOffFontSizeProperty =
            DependencyProperty.Register(nameof(OnOffFontSize), typeof(double), typeof(ZBMSToggleButton), new PropertyMetadata(12)); 
        
        
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(nameof(HeaderFontSize), typeof(double), typeof(ZBMSToggleButton), new PropertyMetadata(12));

        public bool IsLoadingEnabled
        {
            get { return (bool)GetValue(IsLoadingEnabledProperty); }
            set { SetValue(IsLoadingEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingEnabledProperty =
            DependencyProperty.Register(nameof(IsLoadingEnabled), typeof(bool), typeof(ZBMSToggleButton), new PropertyMetadata(true, new PropertyChangedCallback(OnLoadingEnabledCallBack)));

        public static void OnLoadingEnabledCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZBMSToggleButton;
            toggleSwitch.updateLoadingState();
        }

        private void updateLoadingState()
        {
            if (!IsLoadingEnabled)
            {
                VisualStateManager.GoToState(this, "Loaded", false);
                OnOffContent = IsOn ? OnContent : OffContent;
            }
        }
        
        public string LoadingContent
        {
            get { return (string)GetValue(LoadingContentProperty); }
            set { 

                if (value == null)
                {
                    SetValue(LoadingContentProperty, "Loading");
                }
                else
                {
                    SetValue(LoadingContentProperty, value);
                }

            }
        }

        public static readonly DependencyProperty LoadingContentProperty =
            DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(ZBMSToggleButton), new PropertyMetadata("Loading"));

        public string OnContent
        {
            get { return (string)GetValue(OnContentProperty); }
            set {

                if (value == null)
                {
                    SetValue(OnContentProperty, "On");
                }
                else
                {
                    SetValue(OnContentProperty, value);
                }
            }
        }

        public static readonly DependencyProperty OnContentProperty =
            DependencyProperty.Register(nameof(OnContent), typeof(string), typeof(ZBMSToggleButton), new PropertyMetadata("On"));


        public string OffContent
        {
            get { return (string)GetValue(OffContentProperty); }
            set { 
                if (value == null)
                {
                    SetValue(OffContentProperty, "Off");
                }
                else
                {
                    SetValue(OffContentProperty, value);
                }

            }
        }

        public static readonly DependencyProperty OffContentProperty =
            DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(ZBMSToggleButton), new PropertyMetadata("Off"));

        public SolidColorBrush OnStateBackground
        {
            get { return (SolidColorBrush)GetValue(OnStateBackgroundProperty); }
            set
            {

                SetValue(OnStateBackgroundProperty, value);
                UpdateButtonBackground();
            }
        }

        public static readonly DependencyProperty OnStateBackgroundProperty =
            DependencyProperty.Register(nameof(OnStateBackground), typeof(SolidColorBrush), typeof(ZBMSToggleButton), new PropertyMetadata(new SolidColorBrush(Colors.Blue), OnOnStateBackGroundChangedCallBack));


        public static void OnOnStateBackGroundChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZBMSToggleButton;
            toggleSwitch.UpdateButtonBackground();
        }


        private void UpdateButtonBackground()
        {
            _colorBrush = OnStateBackground;
            if (IsOn)
            {
                if (_button != null)
                {
                    _button.Background = _colorBrush;
                }
            }
        }


        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

      
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(ZBMSToggleButton), new PropertyMetadata(null,HeaderTextCallBack));

        public static void HeaderTextCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZBMSToggleButton;
            toggleSwitch.ModifyHeaderVisibility();

        }

        public void ModifyHeaderVisibility()
        {
            if (_header != null)
            {
                if (HeaderText == null)
                {
                    _header.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _header.Visibility = Visibility.Visible;
                }
            }

        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(ZBMSToggleButton), new PropertyMetadata(true,new PropertyChangedCallback( LoadedCallBack)));

        public static void LoadedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZBMSToggleButton;
            toggleSwitch.ModifyLoadingState();
        }

        private void ModifyLoadingState()
        {
            if (IsLoadingEnabled)
            {
                if (IsLoading )
                {
                    VisualStateManager.GoToState(this, "Loading", false);
                    OnOffContent = LoadingContent;
                }
                else
                {
                    VisualStateManager.GoToState(this, "Loaded", false);
                    OnOffContent = IsOn ? OnContent : OffContent;
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "Loaded", false);
                OnOffContent = IsOn ? OnContent : OffContent;
            }

        }

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(ZBMSToggleButton), new PropertyMetadata(false, AvailabilityCallBack));

        public static void  AvailabilityCallBack(DependencyObject sender,DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZBMSToggleButton;
            toggleSwitch.ModifySwitchState();
        }

        private void ModifySwitchState( )
        {
            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnState", false);
                OnOffContent = OnContent;
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
                OnOffContent = OffContent;

            }

            if (IsLoadingEnabled)
            {
                IsLoading = true;
            }
           
        }


        protected override void OnApplyTemplate()
        {
            _button = GetChildTemplate<Button>("Button");
            if (_button == null)
            {
                throw new ArgumentNullException();

            }
            _progressRing = GetChildTemplate<ProgressRing>("ProgressRing");

            if (_progressRing == null)
            {
                throw new ArgumentNullException();
            }

            _onOffText = GetChildTemplate<TextBlock>("OnOffText");
            if (_onOffText == null)
            {
                throw new ArgumentNullException();
            }


            _header = GetChildTemplate<TextBlock>("Header");
            if (_header == null)
            {
                throw new ArgumentNullException();
            }


            _button.Click += (s, eventargs) => Toggled?.Invoke(s, eventargs);
            _button.Click += Button_Clicked;
            
            _button.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(PointerPressedFunction), true);
            _button.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(PointerReleasedFunction), true);

            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnState", false);
                OnOffContent = OnContent;
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
                OnOffContent = OffContent;

            }

            if (IsLoadingEnabled)
            {
                IsLoading = false;
            }
        }
        private  void Button_Clicked(object sender, RoutedEventArgs e)
        {
            IsOn = !IsOn;
                       
        }

        private T GetChildTemplate<T> (string childName) where T: DependencyObject
        {
            var child = GetTemplateChild(childName) as T;
            return child;
        }

       private void PointerPressedFunction(object sender, RoutedEventArgs e)
       {
            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnPointerPressed", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "OffPointerPressed", false);
            }
       }

        private void PointerReleasedFunction(object sender, RoutedEventArgs e)
        {
          //  VisualStateManager.GoToState(this, "PointerReleased", false);
        }


    }
}
