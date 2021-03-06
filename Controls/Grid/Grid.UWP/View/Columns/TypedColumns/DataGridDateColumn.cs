﻿using System;
using System.Reflection;
using Telerik.UI.Xaml.Controls.Grid.Primitives;
using Telerik.UI.Xaml.Controls.Input;
using Telerik.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Telerik.UI.Xaml.Controls.Grid
{
    /// <summary>
    /// An extended <see cref="DataGridTextColumn"/> that presents data of type <see cref="DateTime"/>. 
    /// The column will create a <see cref="DataGridDateFilterControl"/> upon triggering the filtering UI through user input.
    /// </summary>
    public class DataGridDateColumn : DataGridTextColumn
    {
        private static TypeInfo dateTypeInfo = typeof(DateTime).GetTypeInfo();
        private static Type datePickerType = typeof(RadDatePicker);

        private static Style defaultCellEditorStyle;

        internal override Style DefaultCellEditorStyle
        {
            get
            {
                if (defaultCellEditorStyle == null)
                {
                    defaultCellEditorStyle = /* UNO TODO */Controls.Primitives.ResourceHelper.LoadEmbeddedResource(
                        typeof(DataGridTextColumn),
                        "Telerik.UI.Xaml.Controls.Grid.View.Columns.Resources.DefaultDateColumnEditorStyle.xaml",
                        "DefaultColumnEditorStyle") as Style;
                }
                return defaultCellEditorStyle;
            }
        }

        internal override bool CanEdit
        {
            get { return this.PropertyInfoInitialized && this.PropertyInfo.DataType.GetTypeInfo().IsAssignableFrom(dateTypeInfo) && this.CanUserEdit; }
        }

        internal override object GetEditorType(object item)
        {
            return this.CanEdit ? datePickerType : DataGridTextColumn.TextBlockType;
        }

        internal override FrameworkElement CreateEditorContentVisual()
        {
            return new RadDatePicker();
        }

        internal override void PrepareEditorContentVisual(FrameworkElement editorContent, Binding binding)
        {
            editorContent.SetBinding(RadTimePicker.ValueProperty, binding);
        }

        internal override void ClearEditorContentVisual(FrameworkElement editorContent)
        {
            editorContent.ClearValue(RadTimePicker.ValueProperty);
        }

        /// <summary>
        /// Creates the <see cref="DataGridDateFilterControl" /> instance that allows filtering operation to be applied upon this column.
        /// </summary>
        protected internal override DataGridFilterControlBase CreateFilterControl()
        {
            return new DataGridDateFilterControl()
            {
                PropertyName = this.PropertyName
            };
        }
    }
}
