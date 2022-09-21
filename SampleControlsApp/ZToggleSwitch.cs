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
    public sealed class ZToggleSwitch : Control
    {
        private Button _rootButton;
        private TextBlock _onOff;
        private TextBlock _header;
        private SolidColorBrush _colorBrush;

        public event EventHandler<RoutedEventArgs> Toggled;
        public event EventHandler<RoutedEventArgs> OnClick;

        public ZToggleSwitch()
        {
            this.DefaultStyleKey = typeof(ZToggleSwitch);
        }
       
        public double OnOffFontSize
        {
            get { return (double)GetValue(OnOffFontSizeProperty); }
            set { SetValue(OnOffFontSizeProperty, value); }
        }

        public static readonly DependencyProperty OnOffFontSizeProperty =
            DependencyProperty.Register(nameof(OnOffFontSize), typeof(double), typeof(ZToggleSwitch), new PropertyMetadata(12));


        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register(nameof(HeaderFontSize), typeof(double), typeof(ZToggleSwitch), new PropertyMetadata(12));

        public bool IsLoadingEnabled
        {
            get { return (bool)GetValue(IsLoadingEnabledProperty); }
            set { SetValue(IsLoadingEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingEnabledProperty =
            DependencyProperty.Register(nameof(IsLoadingEnabled), typeof(bool), typeof(ZToggleSwitch), new PropertyMetadata(true, new PropertyChangedCallback(OnLoadingEnabledCallBack)));

        public static void OnLoadingEnabledCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.UpdateLoadingState();
        }

        private void UpdateLoadingState()
        {
            if (!IsLoadingEnabled)
            {
                VisualStateManager.GoToState(this, "Loaded", false);
                if (_onOff != null)
                {
                    _onOff.Text = IsOn ? OnContent : OffContent;
                }

            }
        }

        public string LoadingContent
        {
            get { return (string)GetValue(LoadingContentProperty); }
            set
            {
                if (value == null)
                {
                    SetValue(LoadingContentProperty, "Loading");
                }
                else
                {
                    SetValue(LoadingContentProperty, value);
                }
                UpdateOnOffText(LoadingContent);
            }
        }

        public static readonly DependencyProperty LoadingContentProperty =
            DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("Loading"));

        public string OnContent
        {
            get { return (string)GetValue(OnContentProperty); }
            set
            {
                if (value == null)
                {
                    SetValue(OnContentProperty, "On");
                }
                else
                {
                    SetValue(OnContentProperty, value);
                }
                UpdateOnOffText(OnContent);
            }
        }

        public static readonly DependencyProperty OnContentProperty =
            DependencyProperty.Register(nameof(OnContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("On"));

        public string OffContent
        {
            get { return (string)GetValue(OffContentProperty); }
            set
            {
                if (value == null)
                {
                    SetValue(OffContentProperty, "Off");
                }
                else
                {
                    SetValue(OffContentProperty, value);
                   
                }
                UpdateOnOffText(OffContent);
            }
        }

        public static readonly DependencyProperty OffContentProperty =
            DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("Off"));

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
            DependencyProperty.Register(nameof(OnStateBackground), typeof(SolidColorBrush), typeof(ZToggleSwitch), new PropertyMetadata(new SolidColorBrush(Colors.Blue), OnOnStateBackGroundChangedCallBack));

        public static void OnOnStateBackGroundChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.UpdateButtonBackground();
        }

        private void UpdateButtonBackground()
        {
            _colorBrush = OnStateBackground;
            if (IsOn)
            {
                if (_rootButton != null)
                {
                    _rootButton.Background = _colorBrush;
                }
            }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata(null, OnHeaderChanged));

        public static void OnHeaderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.ModifyHeaderVisibility();
        }

        public void ModifyHeaderVisibility()
        {
            if (_header != null)
            {
                if (Header == null)
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
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(ZToggleSwitch), new PropertyMetadata(true, new PropertyChangedCallback(OnLoadingChangedCallBack)));

        public static void OnLoadingChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.ModifyLoadingState();
        }

        private void ModifyLoadingState()
        {
            if (IsLoadingEnabled)
            {
                if (IsLoading)
                {
                    VisualStateManager.GoToState(this, "Loading", false);
                    UpdateOnOffText(LoadingContent);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Loaded", false);
                    UpdateOnOffText(IsOn ? OnContent : OffContent);
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "Loaded", false);
                UpdateOnOffText(IsOn ? OnContent : OffContent);
            }
        }

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(ZToggleSwitch), new PropertyMetadata(false, OnSwitchStateChangedCallBack));

        public static void OnSwitchStateChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.ModifySwitchState();
        }

        private void ModifySwitchState()
        {
            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnState", false);
                UpdateOnOffText(OnContent);
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
                UpdateOnOffText(OffContent);
            }

            if (IsLoadingEnabled)
            {
                IsLoading = true;
            }
        }

        private void UpdateOnOffText(string value)
        {
            if (_onOff != null)
            {
                _onOff.Text = value;
            }
        }

        protected override void OnApplyTemplate()
        {
            _rootButton = GetChildTemplate<Button>("RootButton");
            if (_rootButton == null)
            {
                throw new ArgumentNullException("No child (Button) found");
            }

            _onOff = GetChildTemplate<TextBlock>("OnOffText");
            if (_onOff == null)
            {
                throw new ArgumentNullException("No child (TextBlock) found");
            }

            _header = GetChildTemplate<TextBlock>("Header");
            if (_header == null)
            {
                throw new ArgumentNullException("No child (TextBlock) found");
            }

            _rootButton.Click += (s, eventargs) => Toggled?.Invoke(s, eventargs);
            _rootButton.Click += (s, eventargs) => OnClick?.Invoke(s, eventargs);

            _rootButton.Click += RootButton_Clicked;
            _onOff.Tapped+= RootButton_Clicked; 

            _rootButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(RootButton_PointerPressed), true);

            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnState", false);
                UpdateOnOffText(OnContent);
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
                UpdateOnOffText(OffContent);
            }

            if (IsLoadingEnabled)
            {
                IsLoading = false;
            }
        }

        private void RootButton_Clicked(object sender, RoutedEventArgs e)
        {
            IsOn = !IsOn;
        }
       
        private T GetChildTemplate<T>(string childName) where T : DependencyObject =>
            GetTemplateChild(childName) as T;

        private void RootButton_PointerPressed(object sender, RoutedEventArgs e)
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
    }
}

