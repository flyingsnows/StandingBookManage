//初始化脚本

//注册窗口命令的事件监视
/// 监听NanUI窗口保留的最大化、最小化、还原、退出CSS标记的单击事件
/// 标记：
/// -htmlui-close		关闭窗口
/// -htmlui-maximize		最大化窗口
/// -htmlui-minimize		最小化窗口
(function () {
   
    document.addEventListener('DOMContentLoaded', function () {
        document.body.addEventListener('click', function (e) {
            var region = e.srcElement;
            var cmd = region.className.includes('-htmlui-close') ? 'close' : region.className.includes('-htmlui-minimize') ? 'minimize' :
                region.className.includes('-htmlui-maximize') ? 'maximize' : null;
            if (cmd != null && HtmlUI) {
                HtmlUI.hostWindow[cmd].apply();
                e.preventDefault();
                region.blur();
            }
        }, false);

    }, false);
})();
