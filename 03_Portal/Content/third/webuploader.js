$.fn.yzUploader = function (myOptions) {
    if (myOptions) {
        var typeofPick = typeof myOptions.pick;
        if (typeofPick === "undefined" || typeofPick === "string") {
            var pickerID = $(this).selector;
            myOptions.pick = {
                id: pickerID,
                multiple: false
            }
        } else if (myOptions.pick && typeofPick === "object") {
            myOptions.pick.id = $(this).selector;
        }
    }

    if (!myOptions.server || typeof myOptions.server !== "string")
        throw new Error("需要指定接收文件服务端地址");

    //默认文件大小最多为100M
    var maxFileSize = 100;
    var options = {
        fileNumLimit: 1000,
        // 自动上传。
        auto: true,
        // swf文件路径
        swf: '../pages/js/third/webuploader/Uploader.swf',
        // 文件接收服务端。
        server: null,
        // 选择文件的按钮。可选。
        // 内部根据当前运行时创建，可能是input元素，也可能是flash.
        pick: undefined,
        formData: null,
        duplicate: true,
        fileSingleSizeLimit: 1024 * 1024 * maxFileSize
    };
    var finalOptions = $.extend(true, options, myOptions);
    var exceedSizeFiles = [];
    var deniedFiles = [];
    var emptyFiles = [];
    var duplicateFiles = [];
    var percentages = {};
    var uploader = null;
    try {
        uploader = WebUploader.create(finalOptions);
    } catch (e) {
        if (e.message == 'Runtime Error') {
            $.showError('请安装 Flash 插件以支持文件上传！');
            return;
        }
    };
    uploader.on('uploadSuccess', function (file, result) {
        if (result.state !== "SUCCESS") {
            uploader.removeFile(file);
        }
    });
    uploader.on('uploadError', function (file, reason) {
        $.showError("上传失败。");
    });
    uploader.on("uploadStart", function (file) {

    });
    uploader.on("uploadFinished", function (file) {
        var msgArray = [];
        if (exceedSizeFiles.length > 0) {
            var files = exceedSizeFiles.join("<br/>");
            var maxSize = (finalOptions.fileSingleSizeLimit / (1024 * 1024)).toFixed(0);
            var msg = "下面的文件大小超过#exceedSize#M而不能被上传:<br>#files#".replace("#exceedSize#", maxSize).replace("#files#", files);
            msgArray.push(msg);
            //清空数组
            exceedSizeFiles = [];
        }
        if (deniedFiles.length > 0) {
            var files = deniedFiles.join("<br/>");
            var msg = "下面的文件因为后缀名不正确而不能被上传:<br>#files#".replace("#files#", files);
            msgArray.push(msg);
            deniedFiles = [];
        }
        if (emptyFiles.length > 0) {
            var files = emptyFiles.join("<br/>");
            var msg = "下面的文件内容为空不能被上传:<br>#files#".replace("#files#", files);
            msgArray.push(msg);
            emptyFiles = [];
        }
        if (duplicateFiles.length > 0) {
            var files = duplicateFiles.join("<br/>");
            var msg = "下面的文件已经上传，不能重复上传:<br>#files#".replace("#files#", files);
            msgArray.push(msg);
            duplicateFiles = [];
        }
        if (msgArray.length) {
            var msg = msgArray.join("<br/>");
            $.showWarning(msg);
        }
        percentages = {};
    });
    uploader.on("error", function (errorType) {
        if (arguments.length > 1) {
            if (errorType === "Q_TYPE_DENIED") {
                var file = arguments[1];
                if (file.size === 0) {
                    emptyFiles.push(file.name);
                } else {
                    deniedFiles.push(file.name);
                }
            }
            else if (errorType === "Q_EXCEED_NUM_LIMIT") {
                var limitNum = arguments[1];
                $.showWarning("最多只能上传#qty#个文件。".replace("#qty#", limitNum));
            }
            else if (errorType === "F_EXCEED_SIZE") {
                var file = arguments[2];
                exceedSizeFiles.push(file.name);
            }
            else if (errorType === "F_DUPLICATE") {
                var file = arguments[1];
                duplicateFiles.push(file.name);
            }
        }
    });
    uploader.on('fileQueued', function (file) {
        if (file.getStatus() !== 'invalid') {
            percentages[file.id] = [file.size, 0];
        }
    });
    uploader.on('statuschange', function (cur, prev) {
        // 成功
        if (cur === 'error' || cur === 'invalid') {
            percentages[file.id][1] = 1;
        } else if (cur === 'queued') {
            percentages[file.id][1] = 0;
        }
    });
    uploader.on('uploadProgress', function (file, percentage) {
        percentages[file.id][1] = percentage;
        updateTotalProgress();
    });
    //更新进度
    function updateTotalProgress() {
        var loaded = 0,
            total = 0,
            percent,
            progressWidth;

        $.each(percentages, function (k, v) {
            total += v[0];
            loaded += v[0] * v[1];
        });

        percent = total ? loaded / total : 0;
        progressWidth = Math.round(percent * 100);
        uploader.trigger("updateProgress", progressWidth);
    }
    return uploader;
};