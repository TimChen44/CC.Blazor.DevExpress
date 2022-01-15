using DevExpress.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
	public class CcPopupEdit: DxPopup
	{
		public CcPopupEdit()
		{
			base.ShowFooter = true;
			base.CloseOnEscape = false;
			base.CloseOnOutsideClick = false;
			base.ShowCloseButton = false;
			base.Width = "80%";
		}
	}
}
