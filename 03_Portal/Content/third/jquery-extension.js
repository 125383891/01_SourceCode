
$(function () {


    //判断是否是ie8,如果是,给html追加个class='ie8'以标记.
    var isIE = function (ver) {
        var b = document.createElement('b')
        b.innerHTML = '<!--[if IE ' + ver + ']><i></i><![endif]-->'
        return b.getElementsByTagName('i').length === 1
    }
    if (isIE(8)) {
        $('html').addClass("ie8");
    }
});

function getUrlParameter(str) {
    var obj = {};
    if (str) {
        var arr1 = str.split("&");
        var i = 0, len = arr1.length;
        var arr2;
        for (; i < len; i++) {
            arr2 = arr1[i].split("=");
            if (arr2.length >= 2) {
                obj[arr2[0]] = decodeURIComponent(arr2[1]);
            }
            else {
                obj[arr2[0]] = "";
            }
        }
    }
    return obj;
};
function getUrlParameterByName(name) {
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
};
/*
* jQuery alert&confirm
* -------------------------------------------------- */
(function ($) {
    // alert modal
    $.alert = function (msg, callback) {
        bootbox.alert(msg, function () {
            if (callback) {
                callback();
            }

        });
    };

    // confirm modal
    $.confirm = function (msg, callback, options) {
        options = options || {};
        bootbox.dialog({
            message: msg,
            buttons: {
                confirm: {
                    label: options.confirmLabel || "确认",
                    className: "btn-primary",
                    callback: function () {
                        if (callback) {
                            callback(true);
                        }
                    }
                },
                cancel: {
                    label: options.cancelLabel || "取消",
                    className: "btn-default",
                    callback: function () {
                        if (callback) {
                            callback(false);
                        }
                    }
                }
            }
        });
    };
})(jQuery);


(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    $.setUrlParam = function (uri, name, value) {
        var re = new RegExp("([?&])" + name + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + name + "=" + value + '$2');
        }
        else {
            return uri + separator + name + "=" + value;
        }
    }
    $.removeUrlParam = function (url, name) {
        //prefer to use l.search if you have a location/link object
        var urlparts = url.split('?');
        if (urlparts.length >= 2) {

            var prefix = encodeURIComponent(name) + '=';
            var pars = urlparts[1].split(/[&;]/g);

            //reverse iteration as may be destructive
            for (var i = pars.length; i-- > 0;) {
                //idiom for string.startsWith
                if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                    pars.splice(i, 1);
                }
            }

            url = urlparts[0] + '?' + pars.join('&');
            return url;
        } else {
            return url;
        }
    }
})(jQuery);
/*
* jQuery bindEntity
* -------------------------------------------------- */
(function ($) {
    //form : Jquery Object
    //data : JSON
    $.bindEntity = function (form, data) {
        if (!data) return;
        function _getValue(dataModel, data2) {
            var name = dataModel.shift();
            if (data2[name] && (typeof (data2[name]) == "object")) {
                return _getValue(dataModel, data2[name]);
            } else {
                return data2[name];
            }
        }

        form.find("[data-model]").each(function () {
            var model = $(this).attr("data-model").split(".");
            var val = _getValue(model, data);
            if ($(this).is("select") && $(this).prop("multiple")) {
                if (val) {
                    $(this).val(val.split(","));
                }
            } else {
                //字符串去除前后空格
                if (typeof val == "string") {
                    val = $.trim(val);
                }
                $(this).val(val);
            }
        });
        var bsSelect = $('.bs-select');
        if (bsSelect.length > 0 && bsSelect.selectpicker) {
            bsSelect.selectpicker('refresh');
        }
    };

    //form : Jquery Object
    function buildEntity(form) {
        var obj = {};
        form.find("[data-model]").each(function () {
            var name = $(this).attr("data-model");
            var names = name.split(".");
            var temp;
            var val;
            if ($(this).attr("type") == "checkbox"
				|| $(this).attr("type") == "radio") {
                if ($(this).prop("checked")) {
                    val = $(this).val();
                }
            }
            else {
                val = $.trim($(this).val());
            }
            if (names.length > 1) {
                for (var i = 0; i < names.length; i++) {
                    if (temp) {
                        if (!temp[names[i]]) {
                            if (i == (names.length - 1)) temp[names[i]] = val;
                            else temp[names[i]] = {};
                        }
                        temp = temp[names[i]];
                    } else {
                        if (!obj[names[i]]) {
                            if (i == (names.length - 1)) obj[names[i]] = val;
                            else obj[names[i]] = {};
                        }
                        temp = obj[names[i]];
                    }
                }
            } else {
                obj[name] = val;
            }

        });
        return obj;
    }

    $.buildEntity = buildEntity;

    //页面导出Excel:
    $.exportExcel = function (action, params) {
        var form = $("<form>");//定义一个form表单
        form.attr("style", "display:none");
        form.attr("target", "");
        form.attr("method", "post");
        form.attr("action", action);
        $("body").append(form);//将表单放置在web中
        if (params && params.length > 0) {

            for (var i = 0 ; i < params.length; i++) {
                var input1 = $("<input>");
                input1.attr("type", "hidden");
                input1.attr("name", params[i].name);
                input1.attr("value", params[i].value);
                form.append(input1);
            }
        }
        form.submit();//表单提交 
    };



})(jQuery);
/*
* jQuery drags
* -------------------------------------------------- */
$.fn.drags = function (options) {
    options = $.extend({ handle: "", cursor: "move" }, options);
    var $el = options.handle === "" ? this : this.find(options.handle);
    return $el.css('cursor', options.cursor).on("mousedown", function (e) {

        var $drag = options.handle === "" ? $(this).addClass('draggable') : $(this).addClass('active-handle').parent().addClass('draggable');

        var zIdx = $drag.css('z-index'),
			drgH = $drag.outerHeight(),
			drgW = $drag.outerWidth(),
			posY = $drag.offset().top + drgH - e.pageY,
			posX = $drag.offset().left + drgW - e.pageX;

        $drag.parents().on("mousemove", function (e) {
            $('.draggable').offset({
                top: e.pageY + posY - drgH, left: e.pageX + posX - drgW
            }).on("mouseup", function () {
                $(this).removeClass('draggable').css('z-index', zIdx);
            });
        });

        e.preventDefault();
    }).on("mouseup", function () {
        if (options.handle === "") {
            $(this).removeClass('draggable');
        } else {
            $(this).removeClass('active-handle').parent().removeClass('draggable');
        }
    });
};

/*
 * jQuery ajaxSetup
 * -------------------------------------------------- */
(function ($) {
    var blockQ = [];
    $.ajaxSetup({
        cache: false,
        beforeSend: function (p1) {
            var p2 = this;
            var target = "";
            var index = p2.url.indexOf("?");
            var p = getUrlParameter(p2.url.substring(index + 1));
            if (p && p["target"] && p["target"] != "") {
                target = p["target"];
            }
            blockQ.push(target);
            var blockUIOptions = {
                target: target,
                zIndex: 2147483647,
                boxed: true,
                message: "正在处理，请稍候...",
                overlayColor: "#dbdbdb"
            };
            if (!p2.notShowLoading) {
                $.showBlockUI(blockUIOptions);
            }
        }
        , complete: function (p1) {
            var p2 = this;
            var blockTarget = blockQ.pop();
            if (!p2.notShowLoading && (blockTarget || blockQ.length == 0)) {
                $.hideBlockUI(blockTarget);
            }
            if (p1 && p1.responseJSON && p1.responseJSON.Success == false) {
                var showWarning = $.showWarning || toastr["warning"];
                var showError = $.showError || toastr["error"];
                var resData = p1.responseJSON;
                if (resData.Code == 100) {
                    window.location = "/";
                }
                else if (resData.Code == 200) {
                    showWarning("没有权限进行该操作");
                }
                else if (resData.Code == 300) {
                    showWarning("抱歉，当前帐号登录信息已过期，请刷新页面后重新登录！");
                }
                else if (resData.Message) {
                    showWarning(resData.Message);
                }
                else {
                    showError("服务器程序错误,请稍后再试");
                }
            }
        }
        ,error: function (xhr, textStatus, thrownError) {

        }
    });
})(jQuery);

/*
* 下拉树插件 
*  options 
*   url: ajax数据地址(必填)
*   treeId: 树插件ID(必填)
*   iptText: 文本显示的id,name,data-model(必填)
*   iptValue: 值的id,name,data-model(必填)
*   icon: 图标(可选，默认为fa fa-folder)
*   multiple: 是否复选(可选，默认为false)
*   completeName: 是否显示完整path路径, 默认fasle
*   partiallySelect: 根据返回值判断是否选择该节点，默认false
*   hiddenValue: 隐藏域里填写的值，从数据的entity获得，默认为SysNo
*   change: 树选择发生改变的事件，默认为空
*   loaded: 树加载完成的事件，默认为空
*   notempty:{
		flag:是否集成bootvalidator(单选模式可选，默认false),
		message:错误提示信息
*   childParentMutex: 子项和父项互斥（复选模式下可用可选，默认为false）
	}
* 方法:
	getSelectData:获取选中节点对象集合
	setSelectData: 初始选中方法 传一个对象或对象数组 必须有id，text
	refresh: 控件清空方法
	hasSelected: 判断是否有节点选中
    setDisableNodes：设置禁用项
* -------------------------------------------------- */
; (function ($) {
    var selectedData = {};
    //复选模式
    var multipleMode = function (ele) {
        var $this = ele;
        var settings = $this.data('DropdownTree');
        var $dt_tagdiv = $("<div></div>").addClass('bootstrap-tagsinput').css({ "min-height": "34px", "border-radius": "3px", "padding-right": "20px" }).attr({ 'data-toggle': 'dropdown', 'aria-haspopup': true, 'aria-expanded': true }).prop("id", settings.iptText).appendTo($this),
			$dt_dm = $('<div></div>').addClass('dropdown-menu').attr('aria-labelledby', 'dropdownMenu1').css({ 'width': 'auto', 'min-width': '100%', "padding": "5px 20px", "max-height": "400px", "overflow": "auto" }).appendTo($this),
			$dt_iptValue = $('<input>').attr('data-model', settings.iptValue).prop({ 'type': 'hidden', 'id': settings.iptValue, 'name': settings.iptValue }).appendTo($this),
			$dt_ipt_i = $('<i></i>').addClass('dropdowntree input-icon fa fa-caret-down').css('pointer-events', 'none').appendTo($this),
			$dt_tree = $('<div></div>').prop('id', settings.treeId).appendTo($dt_dm);

        $('#' + settings.treeId).on('click', function (e) {
            e.stopPropagation();
        });
        $dt_tree.on('select_node.jstree', function () {
            var selectNode = $dt_tree.jstree().get_selected(true)[0];
            var selectNodeObj = selectNode.original;
            if (!selectNodeObj.entity) {
                selectNodeObj.entity = selectNode.data;
            }
            !selectedData[settings.treeId] && (selectedData[settings.treeId] = []);
            !isRepeated(selectNodeObj.id, selectedData[settings.treeId]) && selectedData[settings.treeId].push(selectNodeObj);
            addTags(selectedData[settings.treeId], $dt_tagdiv, settings);
            $dt_iptValue.val(getSelectedId(selectedData[settings.treeId], settings.hiddenValue));
            if (settings.childParentMutex) {
                if (!selectedData[settings.treeId]) {
                    selectedData[settings.treeId] = [];
                }
                //设置禁用项
                $('#' + settings.treeId).jstree().disable_node(selectNode);
                if (selectNode.parent != null && selectNode.parent != "" && selectNode.parent != "#") {
                    var parentNode = $dt_tree.jstree().get_node(selectNode.parent);
                    if (parentNode != null && parentNode.original != null) {
                        $('#' + settings.treeId).jstree().disable_node(parentNode);
                    }
                }
                if (selectNode.children && selectNode.children.length > 0) {
                    for (var i = 0; i < selectNode.children.length; i++) {
                        var cldNode = $dt_tree.jstree().get_node(selectNode.children[i]);
                        $('#' + settings.treeId).jstree().disable_node(cldNode);
                    }
                }
            }

        }).on('load_node.jstree', function (event,obj) {
            if (settings.childParentMutex) {
                //设置禁用项
                var curNode = obj.node;
                if (curNode != null) {
                    if (curNode.id != "#") {
                        if (curNode.children && curNode.children.length > 0) {
                            if (settings.disabledNodes.indexOf(curNode.id) >= 0) {
                                //禁用省
                                var hasChild = false;
                                for (var i = 0; i < settings.disabledNodes.length; i++) {
                                    hasChild = curNode.children.indexOf(settings.disabledNodes[i]) >= 0;
                                    if (hasChild) {
                                        break;
                                    }
                                }
                                if (hasChild) {
                                    for (var i = 0; i < settings.disabledNodes.length; i++) {
                                        if (curNode.children.indexOf(settings.disabledNodes[i]) >= 0) {
                                            var cldNode = $dt_tree.jstree().get_node(settings.disabledNodes[i]);
                                            $dt_tree.jstree().disable_node(cldNode);
                                        }
                                    }
                                } else {
                                    for (var i = 0; i < curNode.children.length; i++) {
                                        var cldNode = $dt_tree.jstree().get_node(curNode.children[i]);
                                        $dt_tree.jstree().disable_node(cldNode);
                                    }
                                }
                            } else if (curNode.state.disabled) {
                                for (var i = 0; i < curNode.children.length; i++) {
                                    var cldNode = $dt_tree.jstree().get_node(curNode.children[i]);
                                    $dt_tree.jstree().disable_node(cldNode);
                                }
                            }
                        }
                    } else {
                        //根节点
                        if (settings.disabledNodes && settings.disabledNodes.length > 0) {
                            for (var j = 0; j < settings.disabledNodes.length; j++) {
                                var node = $dt_tree.jstree().get_node(settings.disabledNodes[j]);
                                $dt_tree.jstree().disable_node(node);
                            }
                        }
                    }
                }
            }
        });

        $dt_tagdiv.on('click', 'span', function (e) {
            e.preventDefault();
            e.stopPropagation();
        }).on('click', 'span *[data-role="remove"]', function (e) {
            if (settings.disabled) {
                return
            }
            var delNode = $(this).data('id');
            var delNodeObj = getNodeById(selectedData[settings.treeId], delNode);
            selectedData[settings.treeId].splice($.inArray(delNodeObj, selectedData[settings.treeId]), 1);
            $(this).parent().remove();
            settings.tagremove();
            if (settings.childParentMutex) {
                //启用被禁用项
                $('#' + settings.treeId).jstree().enable_node(delNodeObj);
                if (settings.disabledNodes.indexOf(delNode + "")>=0) {
                    settings.disabledNodes.splice(settings.disabledNodes.indexOf(delNode + ""), 1);
                }
                var nodeObj = $('#' + settings.treeId).jstree().get_node(delNodeObj);
                if (nodeObj.parent != null && nodeObj.parent != "" && nodeObj.parent != "#") {
                    var parentNode = $('#' + settings.treeId).jstree().get_node(nodeObj.parent);
                    if (parentNode != null && parentNode.original != null) {
                        if (parentNode.children != null && parentNode.children.length > 0) {
                            var childSelected = false;
                            //先查看是否已存在被禁用的项
                            for (var i = 0; i < settings.disabledNodes.length; i++) {
                                if (parentNode.children.indexOf(settings.disabledNodes[i]) >= 0) {
                                    childSelected = true;
                                    break;
                                }
                            }
                            if (!childSelected) {
                                //再查看当前页面操作的选中项
                                for (var i = 0; i < parentNode.children.length; i++) {
                                    var cldObj = getNodeById(selectedData[settings.treeId], parentNode.children[i]);
                                    if (cldObj && cldObj.id != null) {
                                        childSelected = true;
                                        break;
                                    }
                                }
                            }
                            if (!childSelected) {
                                $('#' + settings.treeId).jstree().enable_node(parentNode);
                            }
                        }
                    }
                }
                if (nodeObj.children && nodeObj.children.length > 0) {
                    for (var i = 0; i < nodeObj.children.length; i++) {
                        if (settings.disabledNodes.indexOf(nodeObj.children[i]) < 0) {
                            //不存在被禁用的列表中
                            var cldNode = $dt_tree.jstree().get_node(nodeObj.children[i]);
                            $('#' + settings.treeId).jstree().enable_node(cldNode);
                        }
                    }
                }
            }
        });
    };

    //单选模式
    var singleMode = function (ele) {
        var $this = ele;
        var settings = $this.data('DropdownTree');
        var $dt_span = $('<span></span>').addClass('input-icon icon-right').appendTo($this).css('display','block'),
			$dt_iptText = $('<input>').addClass('form-control btn_dropdown_input').attr({ 'data-toggle': 'dropdown', 'data-model': settings.iptText, 'aria-haspopup': true, 'aria-expanded': true }).prop({ 'id': settings.iptText, 'name': settings.iptText, 'readOnly': true }).css({ "border": "1px solid #DEDEDE", "cursor": "pointer" }).val(settings.defaultTitle).appendTo($dt_span),
			$dt_ipt_i = $('<i></i>').addClass('fa fa-caret-down').css('pointer-events', 'none').appendTo($dt_span),
			$dt_dm = $('<div></div>').addClass('dropdown-menu').attr('aria-labelledby', 'dropdownMenu1').css({ 'width': 'auto', 'min-width': '100%', "padding": "5px 20px", "max-height": "400px", "overflow": "auto" }).appendTo($dt_span),
			$dt_iptValue = $('<input>').attr('data-model', settings.iptValue).prop('type', 'hidden').prop('id', settings.iptValue).prop('name', settings.iptValue).appendTo($dt_span),
            $dt_tree = $('<div></div>').prop('id', settings.treeId).appendTo($dt_dm);

        var validator = $this.parents('form').data('bootstrapValidator');

        $dt_tree.on('select_node.jstree', function () {
            var selectObj = $dt_tree.jstree().get_selected(true)[0];
            settings.completeName ? $dt_iptText.val(getCompleteName(selectObj, $dt_tree)) : $dt_iptText.val(selectObj.text);
            if (!settings.oneload) {
                !selectObj.original.entity[settings.hiddenValue] && $dt_iptValue.val("") || $dt_iptValue.val(selectObj.original.entity[settings.hiddenValue]);
            } else {
                $dt_iptValue.val(selectObj.id);
            }
            settings.notempty.flag && validator && validator.revalidateField(settings.iptText);
            selectedData[settings.treeId] = selectObj.original;
        });
        if (settings.notempty.flag) {
            $dt_iptText.attr('data-bv-notempty', true).attr('data-bv-notempty-message', settings.notempty.message);

            validator && validator.addField(settings.iptText);
        }
        if (settings.partiallySelect) {
            $dt_tree.on('click', '.jstree-disabled', function (e) {
                e.preventDefault();
                e.stopPropagation();
            });
        }
    };

    //复选模式添加tag
    var addTags = function (arr, ele, settings) {
        ele.empty();
        arr.forEach(function (obj) {
            var $tag = $('<span></span>').addClass('tag label label-info').css({ "max-width": "90%" }).prop('title', obj.text).appendTo(ele),
				$tagspan = $('<span></span>').css({ "display": "block", "overflow": "hidden", "white-space": "nowrap", "text-overflow": "ellipsis" }).text(obj.text).appendTo($tag),
                $tag_remove = $('<span></span>').attr({ "data-id": obj.id, "data-role": "remove" }).appendTo($tag);
            $tag.css({ paddingRight: settings.disabled ? '10px' : null });
            $tag_remove.toggle(!settings.disabled);
        });
    };

    //获取完整节点名称
    var getCompleteName = function (obj, ele) {
        var nodeArr = ele.jstree().get_path(obj);
        var completename = "";
        nodeArr.forEach(function (name) {
            completename += name + '-';
        });
        return completename.substring(0, completename.length - 1);;
    };

    //遍历树根据数据判断节点是否能被选中
    var judgeTreeSelect = function (data, ele) {
        var $this = ele;
        var settings = $this.data('DropdownTree');
        data.forEach(function (obj) {
            if (!obj.enabled) {
                $('#' + settings.treeId).jstree().disable_node(obj);
            }
        })
    };

    //根据id获取node对象
    var getNodeById = function (arr, id) {
        var data = {}, idstr = id.toString();
        arr.forEach(function (obj) {
            if (idstr == obj.id) {
                data = obj;
            }
        });
        return data;
    };

    //根据select对象的hiddenValue集合
    var getSelectedId = function (arr, str) {
        var data = [];
        for (var i = 0; i < arr.length; i++) {
            if (!arr[i].entity[str]) {
                break;
            } else {
                data.push(arr[i].entity[str]);
            }
        }
        return data;
    };

    //判断是否重复
    var isRepeated = function (id, arr) {
        var ids = [];
        arr.forEach(function (obj) {
            ids.push(obj.id);
        });
        if (ids.indexOf(id) > -1) {
            return true
        } else {
            return false
        }
    };

    //检索功能
    var searchFunc = function (ele) {
        var $this = ele,
            settings = $this.data('DropdownTree'),
            to = false;
        var $dt_tree_q = $('<input>').prop('id', settings.treeId + '_q')
            .addClass('input-sm form-control m5')
            .attr("placeholder", "请输入关键字...")
            .insertBefore($('#' + settings.treeId));
        $dt_tree_q.on('click', function (e) {
            e.stopPropagation();
        }).keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $dt_tree_q.val();
                $('#' + settings.treeId).jstree(true).search(v);
            }, 250);
        });
    };

    //服务端检索功能
    var searchRemote = function (ele) {
        var $this = ele,
            settings = $this.data('DropdownTree'),
            $dt_tree_query = $('<div>').addClass('position-r').insertBefore($('#' + settings.treeId)),
            $dt_tree_q = $('<input>').prop('id', settings.treeId + '_q')
            .addClass('input-sm form-control m5')
            .attr("placeholder", "请输入关键字...")
            .appendTo($dt_tree_query),
            $close_btn = $('<span>').css({
            'position': 'absolute',
            'right': '0',
            'top': '3px',
            'font-size': '18px',
            'color': '#666666',
            'cursor': 'pointer'
        }).addClass('close').text('×').appendTo($dt_tree_query);
        $dt_tree_q.on('click', function (e) {
            e.stopPropagation();
        }).keyup(function (e) {
            if (e.keyCode === 13 || e.keyCode === 8) {
                var keyword = $dt_tree_q.val();
                if (keyword === "" && e.keyCode === 8) {
                    searchBack(ele);
                } else if (keyword === "" && e.keyCode === 13) {
                    return
                } else {
                    $('#' + settings.treeId).jstree().settings.core.data.url = settings.searchRemoteUrl + "?keywords="+keyword;
                    $('#' + settings.treeId).jstree().refresh();
                }
            }
        });
        $close_btn.on('click', function (e) {
            e.stopPropagation();
            $dt_tree_q.val('');
            searchBack(ele);
        });
    };

    var searchBack = function (ele) {
        var $this = ele,
            settings = $this.data('DropdownTree');
        $('#' + settings.treeId).jstree().settings.core.data.url = settings.url;
        $('#' + settings.treeId).jstree().refresh();
    };

    //曝露方法
    var methods = {
        init: function (options) {
            return this.each(function () {
                var $this = $(this);
                var settings = $this.data('DropdownTree');
                if (typeof (settings) == 'undefined') {
                    defaults = {
                        'url': '',
                        'treeId': '',
                        'iptText': '',
                        'iptValue': '',
                        'icon': 'fa fa-folder',
                        'multiple': false,
                        'completeName': false,
                        'partiallySelect': false,
                        'hiddenValue': 'SysNo',
                        'loaded': function () { },
                        'change': function () { },
                        'tagremove': function () { },
                        'disabled': false,
                        'oneload': false,
                        'disabled': false,
                        'defaultTitle': '',
                        'search': false,
                        'searchRemote': false,
                        'searchRemoteUrl': '',
                        'notempty': {
                            flag: false,
                            message: ''
                        },
                        'childParentMutex':false,
                        'disabledNodes':[]
                    };
                    settings = $.extend({}, defaults, options);
                    $this.data('DropdownTree', settings);
                } else {
                    settings = $.extend({}, settings, options);
                }
                var jt_plugins = ["types","search"];                
                $this.addClass('dropdown');
                if (settings.multiple) {//复选
                    multipleMode($this);
                } else {//单选
                    singleMode($this);
                }
                settings.searchRemote && searchRemote($this);
                settings.search && searchFunc($this);
                settings.disabled && $('#' + settings.iptText).attr('data-toggle', '');
                $('#' + settings.treeId).jstree({
                    'core': {
                        'data': {
                            "url": settings.url,
                            "data": function (node) {
                                var specifiedSysNo = settings.specifiedSysNo;
                                var isMaintain = settings.isMaintain;
                                var data = null;
                                if (node.id == "#" && specifiedSysNo) {
                                    data = { "id": specifiedSysNo, "isSpecified": "yes" };
                                    data.limitCategorys = settings.limitCategorys;
                                    data.limitThisCompanyCode = settings.limitThisCompanyCode;
                                    data.isMaintain = isMaintain;
                                    return data;
                                }
                                data = { "id": node.id, "isSpecified": "no" };
                                data.limitCategorys = settings.limitCategorys;
                                data.limitThisCompanyCode = settings.limitThisCompanyCode;
                                data.isMaintain = isMaintain;
                                return data;
                            },
                            "success": function (data) {
                                if (settings.partiallySelect) {
                                    $('#' + settings.treeId).on('redraw.jstree', function () {
                                        settings.partiallySelect && judgeTreeSelect(data, $this);
                                    });
                                }
                            }
                        }
                    },
                    "plugins": jt_plugins,
                    "types": {
                        "default": {
                            "icon": settings.icon
                        }
                    }
                }).on('click', '.jstree-ocl', function (e) {
                    e.stopPropagation();
                }).on('ready.jstree', function () {
                    settings.loaded();
                }).on('changed.jstree', function () {
                    settings.change();
                });
            });
        },

        //destroy
        destroy: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            delete selectedData[settings.treeId];
            $this.removeData('DropdownTree');
            $this.empty();
            return true
        },

        //获取选中节点对象集合
        getSelectData: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            return selectedData[settings.treeId];
        },

        //初始设值方法（选中）
        setSelectData: function (obj) {
            if (typeof (obj) != "object") {
                //console.error("传入参数错误");
                return false
            }
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            selectedData[settings.treeId] = obj;
            if (settings.multiple) {
                addTags(obj, $('#' + settings.iptText), settings);
                $('#' + settings.iptValue).val(getSelectedId(obj, settings.hiddenValue));
            } else {
                if (!obj.entity[settings.hiddenValue]) {
                    return false
                } else {
                    $('#' + settings.iptText).val(obj.text);
                    $('#' + settings.iptValue).val(obj.entity[settings.hiddenValue]);
                }
            }
            return true
        },

        //refresh方法 清空text和value
        refresh: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            if (!settings.multiple) {
                $('#' + settings.iptText).val(settings.defaultTitle);
                $('#' + settings.iptValue).val('');
            } else {
                $('#' + settings.iptText).empty();
                $('#' + settings.iptValue).val('');
            }
            selectedData[settings.treeId] = [];
            $('#' + settings.treeId).jstree().deselect_all();
            return true
        },

        //判断有没有node选中的方法
        hasSelected: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            if (!selectedData[settings.treeId]) {
                return false
            } else {
                return true
            }
        },

        //设置为不可编辑状态 disable
        setTreeDisable: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            settings.disabled = true;
            $('#' + settings.iptText).attr('data-toggle', '').css("cursor", "no-drop");
            return true
        },

        //设置为不可编辑状态 enable
        setTreeEnable: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            settings.disabled = false;
            $('#' + settings.iptText).attr('data-toggle', 'dropdown').css("cursor", "pointer");;
            return true
        },

        //默认初始选择，选择第一个节点数据
        defaultSelect: function () {
            var $this = $(this);
            var settings = $this.data('DropdownTree');
            var id = $("#" + settings.treeId).find("li[role='treeitem']:first").attr("id");
            $this.DropdownTree('setSelectData', $("#" + settings.treeId).jstree().get_node(id).original);
            return true
        },

        //设置禁用项
        setDisableNodes: function (data) {
            if (data != null && data.constructor == Array) {
                var $this = $(this);
                var settings = $this.data('DropdownTree');
                settings.disabledNodes = data;
                //设置禁用项
                //先还原
                var allData = $("#" + settings.treeId).jstree()._model.data;
                for (var porp in allData) {
                    if (typeof (allData[porp]) != "function") {
                        var item = allData[porp];
                        var nodeObj = $("#" + settings.treeId).jstree().get_node(item.id);
                        if (nodeObj) {
                            $('#' + settings.treeId).jstree().enable_node(nodeObj);
                            if (nodeObj.children && nodeObj.children.length > 0) {
                                for (var i = 0; i < nodeObj.children.length; i++) {
                                    var cldObj = $("#" + settings.treeId).jstree().get_node(nodeObj.children[i].id);
                                    if (cldObj) {
                                        $('#' + settings.treeId).jstree().enable_node(cldObj);
                                    }
                                }
                            }
                        }
                    }
                }
                //再禁用
                for (var i = 0; i < data.length; i++) {
                    var nodeObj = $("#" + settings.treeId).jstree().get_node(data[i]);
                    if (nodeObj) {
                        $('#' + settings.treeId).jstree().disable_node(nodeObj);
                    }
                }
            }
        }
    };


    $.fn.DropdownTree = function () {
        var method = arguments[0];
        if (methods[method]) {
            method = methods[method];
            arguments = Array.prototype.slice.call(arguments, 1);
        } else if (typeof (method) == 'object' || !method) {
            method = methods.init;
        } else {
            //console.error('方法 ' + method + ' 在DropdownTree中不存在');
            return this;
        }
        return method.apply(this, arguments);
    };
})(jQuery);

/*
 table 文本超长处理
* -------------------------------------------------- */
(function ($) {
    $.tableTextLong = function (table) {
        var $table = $(table);
        var $th = $table.find("th").css({ "white-space": "nowrap", "word-wrap": "normal" });
        var targetWidth = $table.width() / ($th.length - 2);
        //判断是否有checkbox的列
        if ($table.find('th.select-checkbox').length > 0) {
            $table.find('th.select-checkbox').css("width", "50px");
        }
        //判断
        $th.each(function (idx, obj) {
            if ($(obj).width() > targetWidth) {
                var $tr = $table.find('tr');
                for (var i = 0; i < $tr.length; i++) {
                    var $td = $($tr[i]).find('td').eq(idx);
                    if ($td.children("a,button").hasClass("btn")) {
                        break;
                    }
                    var $div = $("<div>").addClass("table-ellipsis").prop("title", $td.text()).append($td.html()).css({ "max-width": targetWidth, "width": targetWidth });
                    $td.empty().append($div).css("width", targetWidth);
                }
            }
        });
    };
})(jQuery);

/*
 uniform 方法置空
* -------------------------------------------------- */
(function ($) {
    $.fn.uniform = function () {
        return false;
    };
})(jQuery);


/*
 文本输入框根据内容自动变长
* -------------------------------------------------- */
(function ($) {
    $.fn.autoGrowInput = function (o) {
        o = $.extend({
            maxWidth: 1000,
            minWidth: 50,
            comfortZone: 10,
            onChangeWidth: null
        }, o);

        this.filter('input:text').each(function () {

            var minWidth = o.minWidth || $(this).width(),
                val = null,
                input = $(this),
                testSubject = $('<tester/>').css({
                    position: 'absolute',
                    top: -9999,
                    left: -9999,
                    width: 'auto',
                    fontSize: input.css('fontSize'),
                    fontFamily: input.css('fontFamily'),
                    fontWeight: input.css('fontWeight'),
                    letterSpacing: input.css('letterSpacing'),
                    whiteSpace: 'nowrap'
                }),
                check = function () {
                    if (val === (val = input.val())) { return; }

                    // Enter new content into testSubject
                    var escaped = val.replace(/&/g, '&amp;').replace(/\s/g, '&nbsp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    testSubject.html(escaped);

                    // Calculate new width + whether to change
                    var testerWidth = testSubject.width(),
                        newWidth = (testerWidth + o.comfortZone) >= minWidth ? testerWidth + o.comfortZone : minWidth,
                        currentWidth = input.width(),
                        isValidWidthChange = (newWidth < currentWidth && newWidth >= minWidth)
                                             || (newWidth > minWidth && newWidth < o.maxWidth);

                    // Animate width
                    if (isValidWidthChange) {
                        input.width(newWidth);
                    }

                    if (o.onChangeWidth) {
                        o.onChangeWidth(input, newWidth, o);
                    }
                };

            testSubject.insertAfter(input);

            $(this).bind('keyup keydown blur update', check).trigger('update');

        });
        return this;
    };

})(jQuery);
