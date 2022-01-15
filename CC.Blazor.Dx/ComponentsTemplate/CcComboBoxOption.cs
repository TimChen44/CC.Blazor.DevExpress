using DevExpress.Blazor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
    public class CcComboBoxOption<TData, TValue> : DxComboBox<TData, TValue>
    {
        /*
          <DxComboBoxOption Data="@ledgeCxt.QueryPar.YearOptions" @bind-Value="@ledgeCxt.QueryPar.Year" />
         */

        /// <summary>
        /// 设置默认的TextFieldName，避免每个地方都需要设置
        /// </summary>
        public static string TextFieldNameDefault { get; set; } = "Text";
        /// <summary>
        /// 设置默认的ValueFieldName，避免每个地方都需要设置
        /// </summary>
        public static string ValueFieldNameDefault { get; set; } = "Value";

        public CcComboBoxOption() : base()
        {
         
        }

        protected override void OnInitialized()
        {
            this.TextFieldName = TextFieldNameDefault;
            this.ValueFieldName = ValueFieldNameDefault;
            base.OnInitialized();
        }
    }
}
