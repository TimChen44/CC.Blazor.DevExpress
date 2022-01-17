using CC.Blazor.Dx;
using CC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor
{
    public static class ResultExtension
    {
        /// <summary>
        /// 如果结果失败就弹出提示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="boxSrv"></param>
        /// <returns></returns>
        public static bool FailAndPrompt(this Result result, CcToastService toastSrv)
        {
            if (result.IsOK == true) return false;
            toastSrv.Error(result.Message ?? "返回失败");
            return true;
        }

    }
}
