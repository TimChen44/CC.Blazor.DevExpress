using DevExpress.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
    public class CcDxTagBox<TData, TValue> : DxTagBox<TData, TValue>
    {
        /*
                    <CcDxTagBox Data="@Cxt.UserRolesEnum" @bind-Values="AccountUserEditDto.UserRoles" />
 */


        /// <summary>
        /// 设置默认的TextFieldName，避免每个地方都需要设置
        /// </summary>
        public static string TextFieldNameDefault { get; set; } = "Text";
        /// <summary>
        /// 设置默认的ValueFieldName，避免每个地方都需要设置
        /// </summary>
        public static string ValueFieldNameDefault { get; set; } = "Value";

        public CcDxTagBox() : base()
        {

        }

        protected override void OnInitialized()
        {
            this.TextFieldName = TextFieldNameDefault;
            this.ValueFieldName = ValueFieldNameDefault;
            this.ClearButtonDisplayMode = DataEditorClearButtonDisplayMode.Auto;
            base.OnInitialized();
        }

    }
}
