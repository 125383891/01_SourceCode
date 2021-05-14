layer.use("../layer/skin/layer.ext.css", function () {
    layer.layui_layer_extendlayerextjs = !0
});
var yzLayer = function () {
    var self = this;
    self.photos = function (options, loop, key) {
        var yzTab = options.tab;

        if (typeof (options.photos) === 'object') {
            if (options.photos.start >= options.photos.data.length) throw "array index out of bounds";
        }

        if (options.shift === undefined) options.shift = 0;
        if (options.shade === undefined) options.shade = [0.3, '#000', true];
        if (options.needNavArrow === undefined) options.needNavArrow = true;
        if (options.needToolbar === undefined) options.needToolbar = true;

        options.tab = function (pic, layero) {
            if (options.needToolbar === true) {
                var optDivId = 'layui-layer-shade-photo-opt';
                var optDivBgId = 'layui-layer-shade-photo-opt-bg';

                //添加图片的旋转和放大缩小
                var div = $('<div>');
                var divBg = $('<div>');
                divBg.attr({ 'id': optDivBgId });
                div.attr({ 'id': optDivId });
                if (layero.find('.layui-layer-imgprev').length) {
                    div.append('<span><i class="fa fa-chevron-left" data-opt="prev"></i></span>');
                }

                div.append('<span><i class="fa fa-rotate-left" data-opt="rleft"></i></span>')
                    .append('<span><i class="fa fa-search-plus" data-opt="splus"></i></span>')
                    .append('<span><i class="fa fa-search-minus" data-opt="sminus"></i></span>')
                    .append('<span><i class="fa fa-rotate-right" data-opt="rright"></i></span>');

                if (pic.src.indexOf('.yzw.cn') !== -1) {
                    div.append('<span><i class="fa fa-download" data-opt="download"></i></span>');
                }

                if (layero.find('.layui-layer-imgnext').length) {
                    div.append('<span><i class="fa fa-chevron-right" data-opt="next"></i></span>');
                }

                var optCount = div.find('span').length;
                if (optCount == 7) {
                    div.css({ 'width': null, 'margin-left': null });
                    divBg.css({ 'width': null, 'margin-left': null });
                } else {
                    var width = optCount * 100;
                    var marginLeft = -350 + (7 - optCount) * 50;
                    div.css({ 'width': width + 'px', 'margin-left': marginLeft + 'px' });
                    divBg.css({ 'width': width + 'px', 'margin-left': marginLeft + 'px' });
                }

                var content = $('.layui-layer-content');
                var photos = $('.layui-layer-photos');
                var width = parseInt(photos.css('width').replace('px', ''));
                content.attr('data-rotate', '0');
                photos.attr('data-size', '100');
                //绑定点击事件
                div.find('i').unbind('click').click(function () {
                    var $this = $(this);
                    var opt = $this.attr('data-opt');
                    var r = parseInt(content.attr('data-rotate'));
                    var s = parseInt(photos.attr('data-size'));
                    if (opt == 'rleft') {
                        content.removeClass('photo' + r);
                        if ((r - 90) == -360) {
                            r = 0;
                        } else {
                            r -= 90;
                        }
                        content.attr('data-rotate', r).addClass('photo' + r);
                    }
                    if (opt == 'rright') {
                        content.removeClass('photo' + r);
                        if ((r + 90) == 360) {
                            r = 0;
                        } else {
                            r += 90;
                        }
                        content.attr('data-rotate', r).addClass('photo' + r);
                    }
                    if (opt == 'splus') {
                        if (s < 200) {
                            s += 20;
                        }
                        photos.attr('data-size', s).css('width', (width * s / 100) + 'px');
                        layero.prev().resize();
                    }
                    if (opt == 'sminus') {
                        if (s > 20) {
                            s -= 20;
                        }
                        photos.attr('data-size', s).css('width', (width * s / 100) + 'px');
                        layero.prev().resize();
                    }
                    if (opt == 'prev') {
                        $('a.layui-layer-imgprev').click();
                    }
                    if (opt == 'next') {
                        $('a.layui-layer-imgnext').click();
                    }
                    if (opt == 'download') {
                        if (pic.src.indexOf('.yzw.cn') !== -1) {
                            var parser = document.createElement('a');
                            parser.href = pic.src;
                            var downloadUrl = parser.protocol + '//' + parser.host + '/' + 'DownLoadHandler.ashx?FilePath=' + encodeURIComponent(parser.pathname + parser.search) + '&FileName=' + encodeURIComponent(pic.alt);
                            location.href = downloadUrl;
                        } else {
                            //not support
                        }
                    }
                });
                $('#' + optDivId).remove();
                $('#' + optDivBgId).remove();
                $('body').append(divBg).append(div);
                layero.bind('DOMNodeRemoved', function (event, dom) {
                    $('#' + optDivId).remove();
                    $('#' + optDivBgId).remove();
                });
            }
            if (options.needNavArrow !== true) {
                layero.find('.layui-layer-imgsee').hide();
            }
            if (yzTab) {
                yzTab(pic, layero);
            }
        };

        layer.ready(function () {
            //使用相册
            layer.photos(options, loop, key);
        });
    };
};