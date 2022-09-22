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
        private TextBlock _onOffText;
        private TextBlock _headerText;
        private SolidColorBrush _colorBrush;

        public event EventHandler<RoutedEventArgs> Toggled;
        public event EventHandler<RoutedEventArgs> Click;

        public ZToggleSwitch()
        {
            this.DefaultStyleKey = typeof(ZToggleSwitch);
        }


        public static readonly DependencyProperty OnStateBackgroundProperty =
            DependencyProperty.Register(nameof(OnStateBackground), typeof(SolidColorBrush), typeof(ZToggleSwitch), new PropertyMetadata(null, OnStateBackgroundChanged));

        public static void OnStateBackgroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            if (_headerText != null)
            {
                if (Header == null)
                {
                    _headerText.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _headerText.Visibility = Visibility.Visible;
                }
            }
        }
       
        public string LoadingContent
        {
            get { return (string)GetValue(LoadingContentProperty); }
            set { SetValue(LoadingContentProperty, value); }       
        }

        public static readonly DependencyProperty LoadingContentProperty =
            DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("Loading", ContentChanged));

        public string OnContent
        {
            get { return (string)GetValue(OnContentProperty); }
            set { SetValue(OnContentProperty, value);   }
        }

        public static readonly DependencyProperty OnContentProperty =
            DependencyProperty.Register(nameof(OnContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("On", ContentChanged));

        public string OffContent
        {
            get { return (string)GetValue(OffContentProperty); }
            set { SetValue(OffContentProperty, value);}
        }

        public static readonly DependencyProperty OffContentProperty =
            DependencyProperty.Register(nameof(OffContent), typeof(string), typeof(ZToggleSwitch), new PropertyMetadata("Off", ContentChanged));

       
        public SolidColorBrush OnStateBackground
        {
            get { return (SolidColorBrush)GetValue(OnStateBackgroundProperty); }
            set
            {
                SetValue(OnStateBackgroundProperty, value);
                UpdateButtonBackground();
            }
        }

        public static void ContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleSwitch = sender as ZToggleSwitch;
            toggleSwitch.UpdateOnOffText();
        }


        public bool IsLoadingEnabled
        {
            get { return (bool)GetValue(IsLoadingEnabledProperty); }
            set { SetValue(IsLoadingEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingEnabledProperty =
            DependencyProperty.Register(nameof(IsLoadingEnabled), typeof(bool), typeof(ZToggleSwitch), new PropertyMetadata(false, new PropertyChangedCallback(OnLoadingEnabledCallBack)));

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
                UpdateOnOffText();
            }
        }


        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(ZToggleSwitch), new PropertyMetadata(true, new PropertyChangedCallback(OnLoadingChanged)));

        public static void OnLoadingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
                }
                else
                {
                    VisualStateManager.GoToState(this, "Loaded", false);
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "Loaded", false);
            }
            UpdateOnOffText();
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
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
            }

            if (IsLoadingEnabled)
            {
                IsLoading = true;
            }
            else
            {
                UpdateOnOffText();
            }
        }

        protected override void OnApplyTemplate()
        {
            _rootButton = GetChildTemplate<Button>("RootButton");
            if (_rootButton == null)
            {
                throw new ArgumentNullException("No child (Button) found");
            }

            _onOffText = GetChildTemplate<TextBlock>("OnOffText");
            if (_onOffText == null)
            {
                throw new ArgumentNullException("No child (TextBlock) found");
            }

            _headerText = GetChildTemplate<TextBlock>("Header");
            if (_headerText == null)
            {
                throw new ArgumentNullException("No child (TextBlock) found");
            }

            _rootButton.Click += (s, eventargs) => Toggled?.Invoke(s, eventargs);
            _rootButton.Click += (s, eventargs) => Click?.Invoke(s, eventargs);

            _rootButton.Click += RootButton_Clicked;
            _onOffText.Tapped+= RootButton_Clicked; 

            _rootButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(RootButton_PointerPressed), true);

            if (IsOn)
            {
                VisualStateManager.GoToState(this, "OnState", false);
                UpdateButtonBackground();
            }
            else
            {
                VisualStateManager.GoToState(this, "OffState", false);
            }

            if (IsLoadingEnabled)
            {
                IsLoading = false;
            }
            else
            {
                UpdateOnOffText();

            }
            ModifyHeaderVisibility();
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

        private void UpdateOnOffText()
        {
            if (IsLoadingEnabled)
            {
                if (IsLoading)
                {
                    UpdateOnOffText(LoadingContent);
                }
                else
                {
                    if (IsOn)
                    {
                        UpdateOnOffText(OnContent);
                    }
                    else
                    {
                        UpdateOnOffText(OffContent);
                    }
                }
            }
            else
            {
                if (IsOn)
                {
                    UpdateOnOffText(OnContent);
                }
                else
                {
                    UpdateOnOffText(OffContent);
                }
            }
        }

        private void UpdateOnOffText(string value)
        {
            if (_onOffText != null && value != null)
            {
                _onOffText.Text = value;
            }
        }

        private void RootButton_Clicked(object sender, RoutedEventArgs e)
        {
            IsOn = !IsOn;
        }
              
    }
}

