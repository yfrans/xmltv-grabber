using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace XMLTVGrabberWin
{
    public abstract class FormUI
    {
        #region Private members
        private static Dictionary<string, object> m_controls;
        #endregion Private members

        #region Protected methods
        /// <summary>
        /// Enables or disables a control.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="enable"></param>
        protected static void EnableDisableControl(string controlName, bool enable)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) EnableDisableControl(c, enable);
        }
		/// <summary>
		/// Shows or hides a control
		/// </summary>
		/// <param name="controlName"></param>
		/// <param name="enable"></param>
		protected static void ShowHideControl(string controlName, bool show)
		{
			object c;
			if (m_controls.TryGetValue(controlName, out c)) ShowHideControl(c, show);
		}
        /// <summary>
        /// Changes or appends a controls' text.
        /// Depends on the append value.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="text"></param>
        /// <param name="append"></param>
        protected static void ChangeText(string controlName, string text, bool append)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) ChangeText(c, text, append);
        }
        /// <summary>
        /// Marks a check button control as checked,
        /// or as unchecked.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="check"></param>
        protected static void CheckButton(string controlName, bool check)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) CheckButton(c, check);
        }
        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        protected static DialogResult DisplayMessage(string msg, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(msg, caption, buttons, icon);
        }
        /// <summary>
        /// Adds a strings' array to a combo box.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="values"></param>
        protected static void SetComboBoxValues(string controlName, string[] values)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) SetComboBoxValues(c, values);
        }
        /// <summary>
        /// Adds a data to a data grid view.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="data"></param>
        protected static void SetDataGridViewData(string controlName, object data)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) SetDataGridViewData(c, data);
        }
        /// <summary>
        /// Updates current and max values of a progress bar.
        /// This function refers to a progress bar which located alone on the form,
        /// or located on a tool strip bar.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="current"></param>
        /// <param name="max"></param>
        protected static void UpdateProgressBar(string controlName, int current, int max)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) UpdateProgressBar(c, current, max);
        }
        /// <summary>
        /// Adds a value to a checked list Box, and mark the value as checked or unchecked.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="value"></param>
        /// <param name="isChecked"></param>
        protected static void AddCheckedListValue(string controlName, object value, bool isChecked)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) AddCheckedListValue(c, value, isChecked);
        }
        /// <summary>
        /// Adds values to a checked list box. 
        /// Those values will not be marked as checked.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="values"></param>
        protected static void AddCheckedListValues(string controlName, object[] values)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) AddCheckedListValues(c, values);
        }

		protected static void ClearCheckedListBoxValues(string controlName)
        {
            object c;
            if (m_controls.TryGetValue(controlName, out c)) ClearCheckedListBoxValues(c);
        }

		protected static void ScrollToCaret(string controlName)
		{
			object c;
			if (m_controls.TryGetValue(controlName, out c)) ScrollToCaret(c);
		}

		protected static void Invalidate(string controlName)
		{
			object c;
			if (m_controls.TryGetValue(controlName, out c))
				Invalidate(c);
		}
        #endregion Protected methods

        #region Private methods
        private static void GetControls(object control)
        {
            //this function divides all form controls, and the form itself
            //in to groups, and puts those groups in a dictionary
            //containing the control and its name.
            if (control is ToolStrip)
            {
                foreach (ToolStripItem i in ((ToolStrip)control).Items)
                    GetControls(i);
            }
            else if (control is ToolStripItem)
            {
                if (((ToolStripItem)control).Name.Length > 0)
                    m_controls.Add(((ToolStripItem)control).Name, control);
            }
            else if (control is Control)
            {
                foreach (Control c in ((Control)control).Controls) GetControls(c);
                if (((Control)control).Name.Length > 0)
                    m_controls.Add(((Control)control).Name, control);
            }
        }
        //

		private delegate void InvalidateDelegate(object c);
		private static void Invalidate(object c)
		{
			if (((Control)c).InvokeRequired) {
				((Control)c).Invoke(new InvalidateDelegate(Invalidate), new object[] { c });
				return;
			}
			((Control)c).Invalidate();
		}

		private delegate void ScrollToCaretDelegate(object c);
		private static void ScrollToCaret(object c)
		{
			if (c is RichTextBox) {
				RichTextBox rtb = (RichTextBox)c;
				if (rtb.InvokeRequired) {
					rtb.Invoke(new ScrollToCaretDelegate(ScrollToCaret), new object[] { c });
					return;
				}
				rtb.SelectionStart = rtb.Text.Length;
				rtb.ScrollToCaret();
			}
		}

        private delegate void UpdateProgressBarDelegate(object c, int current, int max);
        private static void UpdateProgressBar(object c, int current, int max)
        {
            if (current <= max)
            {
                //if the progress bar located on a tool strip bar.
                if (c is ToolStripItem)
                {
                    if (((ToolStripItem)c).Owner.InvokeRequired)
                    {
                        ((ToolStripItem)c).Owner.Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { c, current, max });
                        return;
                    }
					if (max > -1)
						((ToolStripProgressBar)c).Maximum = max;
                    ((ToolStripProgressBar)c).Value = current;
                }
                //if the progress bar stands alone, on the form.
                else
                {
                    if (((Control)c).InvokeRequired)
                    {
                        ((Control)c).Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { c, current, max });
                        return;
                    }
					if (max > -1)
						((ProgressBar)c).Maximum = max;
                    ((ProgressBar)c).Value = current;
                }
            }
        }

        private delegate void SetDataGridViewDataDelegate(object c, object data);
        private static void SetDataGridViewData(object c, object data)
        {
            if (c is DataGridView)
            {
                if (((DataGridView)c).InvokeRequired)
                {
                    ((DataGridView)c).Invoke(new SetDataGridViewDataDelegate(SetDataGridViewData), new object[] { c, data });
                    return;
                }
                ((DataGridView)c).DataSource = data;
                ((DataGridView)c).Refresh();
            }
        }
        private delegate void EnableDisableControlDelegate(object c, bool enable);
        private static void EnableDisableControl(object c, bool enable)
        {
            if (c is ToolStripItem)
            {
                if (((ToolStripItem)c).Owner.InvokeRequired)
                {
                    ((ToolStripItem)c).Owner.Invoke(new EnableDisableControlDelegate(EnableDisableControl), new object[] { c, enable });
                    return;
                }
                ((ToolStripItem)c).Enabled = enable;
            }
            else
            {
                if (((Control)c).InvokeRequired)
                {
                    ((Control)c).Invoke(new EnableDisableControlDelegate(EnableDisableControl), new object[] { c, enable });
                    return;
                }
                ((Control)c).Enabled = enable;
            }
        }
		private delegate void ShowHideControlDelegate(object c, bool show);
		private static void ShowHideControl(object c, bool show)
		{
			if (c is ToolStripItem) {
				if (((ToolStripItem)c).Owner.InvokeRequired) {
					((ToolStripItem)c).Owner.Invoke(new ShowHideControlDelegate(ShowHideControl), new object[] { c, show });
					return;
				}
				((ToolStripItem)c).Visible = show;
			}
			else {
				if (((Control)c).InvokeRequired) {
					((Control)c).Invoke(new ShowHideControlDelegate(ShowHideControl), new object[] { c, show });
					return;
				}
				((Control)c).Visible = show;
			}
		}
        private delegate void ChangeTextDelegate(object c, string txt, bool append);
        private static void ChangeText(object c, string txt, bool append)
        {
            if (c is ToolStripItem)
            {
                if (((ToolStripItem)c).Owner.InvokeRequired)
                {
                    ((ToolStripItem)c).Owner.Invoke(new ChangeTextDelegate(ChangeText), new object[] { c, txt, append });
                    return;
                }
                if (!append) ((ToolStripItem)c).Text = txt;
                else ((ToolStripItem)c).Text += txt;
            }
            else
            {
                if (((Control)c).InvokeRequired)
                {
                    ((Control)c).Invoke(new ChangeTextDelegate(ChangeText), new object[] { c, txt, append });
                    return;
                }
				if (!append) ((Control)c).Text = txt;
				else ((Control)c).Text += txt;
            }
        }
        private static void CheckButton(object c, bool check)
        {
            if (c is ToolStripButton) ((ToolStripButton)c).Checked = check;
        }
        private static void SetComboBoxValues(object c, string[] values)
        {
            if (c is ToolStripItem) ((ToolStripComboBox)c).Items.AddRange(values);
            else ((ComboBox)c).Items.AddRange(values);
        }
		private delegate void ClearCheckedListBoxValuesDelegate(object c);
		private static void ClearCheckedListBoxValues(object c)
		{
			if (c is CheckedListBox) {
				if (((CheckedListBox)c).InvokeRequired) {
					((CheckedListBox)c).Invoke(new ClearCheckedListBoxValuesDelegate(ClearCheckedListBoxValues), new object[] { c });
					return;
				}
				((CheckedListBox)c).Items.Clear();
			}
		}
		private delegate void AddCheckedListValueDelegate(object c, object value, bool isChecked);
        private static void AddCheckedListValue(object c, object value, bool isChecked)
        {
			if (c is CheckedListBox) {
				if (((CheckedListBox)c).InvokeRequired) {
					((CheckedListBox)c).Invoke(new AddCheckedListValueDelegate(AddCheckedListValue), new object[] { c, value, isChecked });
					return;
				}
				((CheckedListBox)c).Items.Add(value, isChecked);
			}
        }
		private delegate void AddCheckedListValuesDelegate(object c, object[] values);
        private static void AddCheckedListValues(object c, object[] values)
        {
			if (c is CheckedListBox) {
				if (((CheckedListBox)c).InvokeRequired) {
					((CheckedListBox)c).Invoke(new AddCheckedListValuesDelegate(AddCheckedListValues), new object[] { c, values });
					return;
				}
				((CheckedListBox)c).Items.AddRange(values);
			}
        }
        #endregion Private methods

        #region Public methods

        /// <summary>
        /// Collects all form controls, and the form itself
        /// in to a dictionary.
        /// This function has to be called first.
        /// </summary>
        /// <param name="control"></param>
        public static void Initialize(Control control)
        {
            m_controls = new Dictionary<string, object>();
            GetControls(control);
        }
        /// <summary>
        /// Pops a message box, showing an information.
        /// Message box button is OK.
        /// </summary>
        /// <param name="msg"></param>
        public static void Message(string msg)
        {
            DisplayMessage(msg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Pops a message box, showing a WH question.
        /// Message box buttons are YES and No.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static DialogResult Ask(string q)
        {
            return DisplayMessage(q, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion Public methods
    }
}
