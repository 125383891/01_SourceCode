(function ($) {

    $(function () {
        //让modal支持拖动
        //var modals = $(".modal[role='dialog']");
        //modals.drags({ handle: '.modal-header' });
    });

    $.parseAjaxError = function (data) {
        if (typeof data == "object" && data.Success === false) {
            return data;
        }
        if (typeof data == "string" && data.indexOf('"Success":false') >= 0) {
            return $.parseJSON(data);
        }
        return null;
    };
    var _dialogs = []
    $.getCurrentDialog = function () {
        var index = _dialogs.length - 1;
        return _dialogs[index];
    };

    //参数data：给调用方的数据
    $.closeCurrentDialog = function (data) {
        var currentModal = this.getCurrentDialog();
        if (currentModal) {
            //触发自定义事件
            currentModal.trigger("close.custom.modal", data);
        }
        var $dismiss = currentModal.find("[data-dismiss='modal']:first");
        if ($dismiss.length > 0)
            $dismiss.click();
        else {
            currentModal.modal("hide");
        }
    }
    $.showCustomDialog = function (path, data, callback, options) {
        data = data || {};
        options = options || {};
        var contentType = 'application/x-www-form-urlencoded';
        if (options.method === 'POST') {
            contentType = 'application/json';
            data = $.toJSON(data)
        }
        var me = this;
        $.ajax({
            method: options.method || "GET",
            contentType: contentType,
            cache: false,
            url: path,
            data: data,
            success: function (html) {
                var ajaxError = $.parseAjaxError(html);
                if (ajaxError) {
                    return;
                }

                var html = $(html);
                var m = html.modal({ backdrop: "static", show: false });
                m.drags({ handle: '.modal-header' });
                _dialogs.push(m);
                m.on("hidden.bs.modal", function (e) {
                    $(e.target).remove();
                    $("script.in[type!='text/x-handlebars-template']").remove();
                    _dialogs.pop();
                });
                m.on("show.bs.modal", function (e) {
                    //控制modal-body的最大高度
                    var windowHeight = $(window).height();
                    var modalHeight = windowHeight * 0.7;
                    if (modalHeight < 300) {
                        modalHeight = 300;
                    }
                    $(e.target).find(".modal-body").css({
                        "max-height": modalHeight + "px",
                        "overflow-y": "auto"
                    });

                    if (options.showCallback) {
                        options.showCallback(m);
                    }
                });
                //自定义事件
                m.on("close.custom.modal", function (e, data) {
                    if (callback) callback(data);
                });
                m.on("shown.bs.modal", function () {
                    var sel = document.selection;
                    if (sel) {
                        sel.empty();
                    }

                    if (options.shownCallback) {
                        options.shownCallback(m);
                    }
                });
                m.modal("show");
            }
        });
    };

    $.showSuccess = function (msg) {
        if (!msg) {
            msg = "操作成功。";
        }
        Notify(msg, 'bottom-right', '30000', 'success', 'fa-check', true);
    };

    $.showWarning = function (msg) {
        Notify(msg, 'bottom-right', '30000', 'warning', 'fa-warning', true);
    };

    $.showError = function (msg) {
        Notify(msg, 'bottom-right', '30000', 'danger', 'fa-times', true);
    };

    $.showBlockUI = function (options) {
        options = $.extend(true, { boxed: true }, options);
        var html = '';
        if (options.animate) {
            html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '">' + '<div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' + '</div>';
        } else if (options.iconOnly) {
            html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/Content/img/loading-spinner-grey.gif"  style="vertical-align:middle"></div>';
        } else if (options.textOnly) {
            html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span id="block-text">&nbsp;&nbsp;' + (options.message ? options.message : '正在处理，请稍候...') + '</span></div>';
        } else {
            html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="/Content/img/loading-spinner-grey.gif" style="vertical-align:middle"><span id="block-text">&nbsp;&nbsp;' + (options.message ? options.message : '正在处理，请稍候...') + '</span></div>';
        }

        if (options.target) { // element blocking
            var el = $(options.target);
            if (el.height() <= ($(window).height())) {
                options.cenrerY = true;
            }
            el.block({
                message: html,
                baseZ: options.zIndex ? options.zIndex : 10050,
                centerY: options.cenrerY !== undefined ? options.cenrerY : false,
                css: {
                    top: '10%',
                    border: '0',
                    padding: '0',
                    backgroundColor: 'none'
                },
                overlayCSS: {
                    backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                    opacity: options.boxed ? 0.05 : 0.1,
                    cursor: 'wait'
                },
                blockMsgClass: "blockmsg-fixed"
            });
        } else { // page blocking
            $.blockUI({
                message: html,
                baseZ: options.zIndex ? options.zIndex : 10050,
                css: {
                    border: '0',
                    padding: '0',
                    backgroundColor: 'none'
                },
                overlayCSS: {
                    backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                    opacity: options.boxed ? 0.05 : 0.1,
                    cursor: 'wait'
                },
                blockMsgClass: "blockmsg-fixed"
            });
        }
    },

    $.hideBlockUI = function (target) {
        
        if (target) {
            $(target).unblock({
                onUnblock: function () {
                    $(target).css('position', '');
                    $(target).css('zoom', '');
                }
            });
        } else {
            $.unblockUI();
        }
    };

    $.getSelectedRowsData = function (table) {
        var rows = [];
        var dataTable = $(table).DataTable();
        $('tbody > tr.selected', table).each(function () {
            rows.push(dataTable.row($(this)).data());
        });

        return rows;
    }

    //金额显示为千分位
    //金额显示为千分位
    $.GetformatNum = function (str, num) {
        if (isNaN(num)) {
            num = 2;
        }
        var s = "";
        for (var i = 0; i < num; i++) {
            s += "0";
        }
        if (!isNaN(str)) {
            var newStr = "";
            var count = 0;
            var isf = false; //是否负数
            var str = str + "";
            if (str.indexOf("-") == 0) { //第一个是"-"号
                str = str.substr(1, str.length - 1);
                isf = true;
            }

            str = parseFloat(str).toFixed(num) + ""; //四舍五入


            if (str.indexOf(".") == -1) {
                for (var i = str.length - 1; i >= 0; i--) {
                    if (count % 3 == 0 && count != 0) {
                        newStr = str.charAt(i) + "," + newStr;
                    } else {
                        newStr = str.charAt(i) + newStr;
                    }
                    count++;
                }
                str = newStr + "." + s; //自动补小数点后位数
                //如果是负数，返回是在前添加"-"
                return isf ? "-" + str : str;
            }
            else {
                for (var i = str.indexOf(".") - 1; i >= 0; i--) {
                    if (count % 3 == 0 && count != 0) {
                        newStr = str.charAt(i) + "," + newStr;  //碰到3的倍数则加上“,”号
                    } else {
                        newStr = str.charAt(i) + newStr; //逐个字符相接起来
                    }
                    count++;
                }
                str = newStr + (str + s).substr((str + s).indexOf("."), (num + 1));
                //如果是负数，返回是在前添加"-"
                return isf ? "-" + str : str;
            }
        } else {
            return "0." + s;
        }
    };

})(jQuery);