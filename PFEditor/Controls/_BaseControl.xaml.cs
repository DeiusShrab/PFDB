﻿using System.Windows.Controls;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for BaseControl.xaml
  /// </summary>
  public partial class BaseControl : UserControl
  {
    public BaseControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;
    }
  }
}
