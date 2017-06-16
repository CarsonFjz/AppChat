(function ($) {
    var messageDates = [];
    var expressionArray = [];
    //公用函数类
    var baseTool = {
        newGuid: function () {
            var guid = '';
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        },
    }
    //全局枚举
    var _messageType = {
        text: 0,
        image: 1,
        file: 2,
        audio: 3,
    }
    var _messageSendBy = {
        mine: 0,
        others: 1,
    }
    var initViewOtions = {
        transport: {
            get: '#',
            read: '#',
            send: '#',
            expression: '#',
            time: '#',
            upload: '#',
            download: '#',
        },
        signalR: {
            enable: false,
            address: '#',
            token: '',
        },
        height: 900,
        ticketId: '',
    };
    //全局变量
    var time = '';
    var _lastTime = new Date();
    var _chatArea;
    String.prototype.toHtml = function () {
        var text = this;
        var reg = /<br \/>/g;
        var imgStrReg = /\[(.*?)\]/g;
        var imgStrArray = text.match(imgStrReg);
        $.each(imgStrArray, function (key, val) {
            var title = val.substring(1, val.length - 1);
            var expression = expressionArray.singleOrDefault(function (obj) { return obj.Title == title });
            if (!$.isEmptyObject(expression)) {
                var img = '<img title="' + expression.Title + '" src="' + expression.Path + '" />';
                text = text.replace(val, img);
            }
        })
        return text.replace(/\\r\\n/g, "<br/>").replace(/\\s/g, "&nbsp;");
    };

    String.prototype.toText = function () {
        var html = this;

        //1. 将表情的img标签替换为文字
        var imgReg = /<img\b[^>]*>/g;
        var array = html.match(imgReg)
        $.each(array, function (key, val) {
            var title = $(val).attr('title');
            html = html.replace(val, '[' + title + ']');
        });

        //2. 去除所有多余的HTML标签
        var htmlReg = /<[^>]*>/g;
        array = html.match(htmlReg);
        $.each(array, function (key, val) {
            html = html.replace(val, "");
        });

        return html.replace(/<br\s*\/?>/gi, "\r\n").replace(/&nbsp;/g, "\s");
    };

    Array.prototype.sortDesc = function (propertyName) {
        if (propertyName == null || propertyName == '')
            return this.sort(function (object1, object2) {
                if (object1 > object2) return -1;
                if (object1 < object2) return 1;
                return 0;
            });
        else
            return this.sort(function (object1, object2) {
                var value1 = object1[propertyName];
                var value2 = object2[propertyName];
                if (value1 > value2) return -1;
                if (value1 < value2) return 1;
                return 0;
            })
    }

    Array.prototype.sortAcs = function (propertyName) {
        if (propertyName == null || propertyName == '')
            return this.sort(function (object1, object2) {
                if (object1 < object2) return -1;
                if (object1 > object2) return 1;
                return 0;
            });
        else
            return this.sort(function (object1, object2) {
                var value1 = object1[propertyName];
                var value2 = object2[propertyName];
                if (value1 < value2) return -1;
                if (value1 > value2) return 1;
                return 0;
            })
    }

    Array.prototype.find = function (func) {
        var temp = [];
        for (var i = 0; i < this.length; i++) {
            if (func(this[i])) {
                temp[temp.length] = this[i];
            }
        }
        return temp;
    }

    Array.prototype.singleOrDefault = function (func) {
        var temp = [];
        for (var i = 0; i < this.length; i++) {
            if (func(this[i])) {
                temp[temp.length] = this[i];
            }
        }
        if (temp.length > 1)
            return null;
        return temp[0];
    }

    var getMessage = function (staffId) {
        var readUrl = initViewOtions.transport.get + '?ticketId=' + initViewOtions.ticketId;
        $.ajax({
            url: readUrl,
            type: 'GET',
            async: false,
            success: function (response) {
                if (response != null && response.length > 0) {
                    //message.Messages = message.Messages.concat(data.Messages);
                    //message.Messages.sortAcs('SendTime');
                    $.each(response, function (key, val) {
                        if (val.MessageType == _messageType.file) {
                            val.CommentText = "<div class='file'><div class='arrow'></div><div class='file_bd'><div class='cover'><i></i></div>" +
                                    "<div class='cont'><p>" + val.Attachment.AttachmentName + "</p><div class='opr'><span>" + Number(val.Attachment.AttachmentSize / (1024 * 1024)).toFixed(2) +
                                    "M</span><span>|</span><a href='../Attachment/DownloadAttachment?attachmentId=" + val.Attachment.ID + "'>下载</a></div>" +
                                    "</div><div></div>"
                        }
                        showMessageView(val);
                    })
                }
                else if (response.length == 0) {
                    var $main_massage = $('#div_message_content_main');
                    var $tip_message = $('<div class="empty">暂无聊天记录</div>');
                    $main_massage.prepend($tip_message);
                }
            }
        })
    }

    var initSignalR = function () {
        var conn = $.hubConnection(initViewOtions.signalR.address + "/signalR/hub", null);
        var signalRHub = conn.createHubProxy("layimHub");
        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + initViewOtions.signalR.token);
            },
            error: function (xhr, status, e) {
                if (xhr.status === 401 && xhr.statusText === "Unauthorized") {
                    //登录过期,刷新页面返回登录界面
                    parent.location.reload();
                }
            },
        });

        //连接hub服务
        conn.start({ transport: "longPolling" });

        var setInterval = -1;

        //重写重连事件
        conn.disconnected(function () {
            if (setInterval === -1) {
                setInterval = window.setInterval(function () {
                    console.log("Reconnecting ,Time: " + new Date());
                    conn.start({ transport: "longPolling" });
                }, 30000);
                return;
            }
        });

        conn.stateChanged(function (change) {
            if (change.newState == $.signalR.connectionState.connecting) {
                console.log("Connecting, Time: " + new Date());
            }

            if (change.newState == $.signalR.connectionState.connected) {
                $('#btn_send').attr("disabled", false);
                window.clearInterval(setInterval);
                setInterval = -1;
                console.log("Connected Successfully, Time: " + new Date());
                console.log("------------------------------------------------------------------------------------------------------------------------------------------");
            }

            if (change.newState == $.signalR.connectionState.reconnecting) {
                $('#btn_send').attr("disabled", true);
                conn.stop();
            }

            if (change.newState == $.signalR.connectionState.disconnected) {
                $('#btn_send').attr("disabled", true);
                console.log("Connection Failed, Time: " + new Date());
            }
        })

        signalRHub.on("receiveComment", function (ticketId, response) {
            if (ticketId != initViewOtions.ticketId) {
                return; //不是同一个dig
            }
            showMessageView(response);
            //ajax请求后台表示已读
            $.ajax({
                url: initViewOtions.transport.read,
                type: 'POST',
                data: { "TicketID": ticketId, "CommentID": response.ID },
                dataType: 'json',
                async: true,
                success: function (response) {
                    if (response) {

                    }
                },
                error: function (e) {
                    console.log(e);
                }
            })
        })
    };

    //消息发送
    var send = {
        sendTextMessage: function (options) {
            if ($('#btn_send').is("disabled")) {
                return;
            }

            var innerHTML = options[0].innerHTML;
            innerHTML = innerHTML.toText();
            if (innerHTML == "") {
                return;
            }
            if (innerHTML.length > 250) {
                showTips("内容过长");
                return;
            }
            var data = { 'TicketID': initViewOtions.ticketId, 'Comment': innerHTML, 'MessageType': _messageType.text };
            $.ajax({
                url: initViewOtions.transport.send,
                type: 'POST',
                data: data,
                dataType: 'json',
                async: false,
                success: function (response) {
                    if (response.IsSuccessed) {
                        showMessageView(response.Data);
                        options.empty();
                        renderMessagesView.scrollPosition();
                    }
                    else {
                        window.parent.showMessageWindow("提示信息", response.ErrorMsg);
                    }
                },
                error: function (e) {
                    console.log(e);
                }
            })
        },
        sendFileMessage: function (options, elementId, messageType) {
            if ($('#btn_send').is("disabled")) {
                return;
            }

            data = { 'TicketID': initViewOtions.ticketId, 'AttachmentId': options, 'MessageType': messageType };
            $.ajax({
                url: initViewOtions.transport.send,
                type: 'POST',
                data: data,
                dataType: 'json',
                async: false,
                success: function (response) {
                    if (response.Data.MessageType == _messageType.image) {
                        var image = _chatArea.find('#' + elementId);
                        image.parent().find('.img-loading').remove();
                        image.parent().find('div').remove();
                        image.attr({ 'title': response.Data.Attachment.Title, 'src': response.Data.AttachmentBase64, 'data-image': '../Attachment/GetImage?attachmentId=' + options, 'id': response.Data.Attachment.Id });

                        var parent = image.parent().parent();
                        var prev = parent.prev();
                        if (prev.attr('class') == 'date') {
                            prev[0].innerHTML = response.Data.CreatedDate;
                        }
                    }
                    else if (response.Data.MessageType == _messageType.audio) {
                        var audio = _chatArea.find('#' + elementId);
                        var parent = audio.find('#sending').parent();
                        audio.find('#sending').remove();
                        var durationView = $('<p style="position:absolute;left:-25px;color:#000;font-family:微软雅黑">' + audio.attr('data-duration') + '\'\' </p>');
                        parent.append(durationView);
                    }
                },
                error: function (e) {
                    console.log(e);
                }
            })
        },
        sendFile: function (fileObj, messageType) {
            if ($('#btn_send').is("disabled")) {
                return;
            }

            var allowExtension = [".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".vsd", ".vsdx", ".pdf",
                                  ".zip", ".rar", ".mp3", ".mp4", ".avi", ".rmvb"];
            //获取待上传文件后缀名
            var fileExtensionName = fileObj.name.substr(fileObj.name.lastIndexOf(".")).toLowerCase();//获得文件后缀名
            //文件格式是否允许
            if (allowExtension.indexOf(fileExtensionName) == -1) {
                alert('不允许上传该文件格式');
                return;
            }
            //文件名称长度是否合法
            if (fileObj.name.length > 50) {
                alert("文件名过长");
                return;
            }
            //文件大小判断
            if (fileObj.size / (1024 * 1024) > 10) {
                alert("文件过大");
                return;
            }
            //FormData对象
            var form = new FormData();
            //加载文件到FormData对象
            form.append("file", fileObj, fileObj.name);
            //上传地址
            var uploadUrl = initViewOtions.transport.upload;
            //判断上传的是文件还是图像
            if ('.jpg.jpeg.png'.indexOf(fileExtensionName) == -1) {
                var main_massage = $('#div_message_content_main');
                var fileId = baseTool.newGuid();
                var content = $('<div class="file" id="' + fileId + '">' +
                                    '<div class="arrow"></div>' +
                                    '<div class="file_bd">' +
                                        '<div class="cover">' +
                                            '<i></i>' +
                                        '</div>' +
                                        '<div class="cont">' +
                                            '<p>' + fileObj.name + '</p>' +
                                            '<div class="opr">' +
                                                '<span>' + (fileObj.size / 1024 / 1024).toFixed(2) + 'M</span>' +
                                                '<span>|</span>' +
                                                '<a href="javascript:void(0)">取消</a>' +
                                            '</div>' +
                                            '<progress id="progressBar" class="progressBar" value="0" max="100"></progress>' +
                                        '</div>' +
                                    '<div>' +
                                '</div>');
                main_massage.append(renderMessagesView.file.mine('', content));
                renderMessagesView.scrollPosition();
                var xhr = new XMLHttpRequest();
                content.find("a").on("click", function () {
                    if (this.innerText == "取消") {
                        xhr.abort();
                        this.innerText = "已取消";
                    }
                })
                xhr.open("post", uploadUrl, true);
                xhr.upload.onprogress = function (e) {
                    var progressBar = main_massage.find("#" + fileId + " progress")[0];
                    // e.total是需要传输的总字节，e.loaded是已经传输的字节。如果e.lengthComputable不为真，则event.total等于0
                    if (e.lengthComputable) {
                        progressBar.max = e.total;
                        progressBar.value = e.loaded;
                        if (progressBar.max == progressBar.value) {
                            var download = main_massage.find("#" + fileId + " .opr a");
                            var progressBar = main_massage.find("#" + fileId + " progress");
                            progressBar.remove();
                            download.html("上传成功");
                        }
                    }

                }
                xhr.onreadystatechange = function () {
                    var download = main_massage.find("#" + fileId + " .opr a");
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        try {
                            var response = eval("(" + xhr.response + ")");
                            download.attr('href', initViewOtions.transport.download + '?attachmentId=' + response.Item3[0].ID);
                            send.sendFileMessage(response.Item3[0].ID, null, messageType);
                            var download = main_massage.find("#" + fileId + " .opr a");
                            download.html("下载");
                        }
                        catch (ex) {
                            download.html('上传失败');
                            download.css("color", "red");
                            var fileDiv = main_massage.find("#" + fileId);
                            fileDiv.css("border-color", "red");
                            fileDiv.find("progress").hide();
                            fileDiv.find("p").css("color", "red");
                            var sendAgain = $('<a style="cursor:pointer;background: url(../Content/Images/weChat.png) 0 -1270px;position:absolute;left:-30px;top:40%;width:22px;height:22px"></a>')
                            content.append(sendAgain);
                            sendAgain.on('click', function () {
                                sendAgain.remove();
                                fileDiv.find("progress").show();
                                fileDiv.css("border-color", "#62a8ea");
                                fileDiv.find("p").css("color", "#000");
                                download.css("color", "#0c88ff");
                                download.html("取消");
                                xhr.open("post", uploadUrl, true);
                                xhr.send(form);
                            })
                        }
                        xhr.abort();//释放XMLHttpRequest对象
                    }
                    else if (xhr.readyState == 4 && xhr.status != 200) {
                        download.html('上传失败');
                        download.css("color", "red");
                        var fileDiv = main_massage.find("#" + fileId);
                        fileDiv.css("border-color", "red");
                        fileDiv.find("progress").hide();
                        fileDiv.find("p").css("color", "red");
                        var sendAgain = $('<a style="cursor:pointer;background: url(../Content/Images/weChat.png) 0 -1270px;position:absolute;left:-30px;top:40%;width:22px;height:22px"></a>')
                        content.append(sendAgain);
                        sendAgain.on('click', function () {
                            sendAgain.remove();
                            fileDiv.find("progress").show();
                            fileDiv.css("border-color", "#62a8ea");
                            fileDiv.find("p").css("color", "#000");
                            download.css("color", "#0c88ff");
                            download.html("取消");
                            xhr.open("post", uploadUrl, true);
                            xhr.send(form);
                        })
                    }

                }
                xhr.onerror = function (e) {

                }
                xhr.send(form);
            }
            else {
                var reader = new FileReader();
                reader.onload = function () {
                    var image_id = baseTool.newGuid();
                    showMessageView({ 'ID': '', 'Comment': '', 'SendBy': 'Mine', 'CreatedDate': $.formatterDateTime(new Date()), 'MessageType': _messageType.image, 'AttachmentBase64': this.result, 'Attachment': { 'AttachmentName': form.localName, 'ID': image_id } }, $.formatterDate2(new Date()));
                    var loading = $("<img src='../Content/Images/hrc-loading.gif' style='position: absolute;left: 40%;top: 42%;width:32px;height:32px' class='img-loading'/><div style='position:absolute;top:0px;width:100%;height:100%;background: #000;opacity: .5;border-radius:10px'>");
                    $('#' + image_id).after(loading);
                    //XMLHttpRequest 对象
                    var xhr = new XMLHttpRequest();
                    xhr.open("post", uploadUrl, true);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            try {
                                var response = eval("(" + xhr.response + ")");
                                $('#' + image_id, window.parent.document).attr('id', response.Item3[0].ID);
                                send.sendFileMessage(response.Item3[0].ID, image_id, _messageType.image);
                            }
                            catch (ex) {
                                var imageDiv = $('#' + image_id).parent();
                                imageDiv.css('boder', 'soild 1px red');

                                $(imageDiv.find('.img-loading')).hide();
                                var sendAgain = $('<a style="cursor:pointer;background: url(../Content/Images/weChat.png) 0 -1270px;position:absolute;left:-30px;top:42%;width:22px;height:22px"></a>')
                                imageDiv.append(sendAgain);
                                sendAgain.on('click', function () {
                                    sendAgain.remove();
                                    imageDiv.css('boder', 'none');
                                    $(imageDiv.find('.img-loading')).show();
                                    xhr.open("post", uploadUrl, true);
                                    xhr.send(form);
                                })
                            }
                            xhr.abort();
                        }
                        if (xhr.readyState == 4 && xhr.status != 200) {
                            var imageDiv = $('#' + image_id).parent();
                            imageDiv.css('boder', 'soild 1px red');

                            $(imageDiv.find('.img-loading')).hide();
                            var sendAgain = $('<a style="cursor:pointer;background: url(../Content/Images/weChat.png) 0 -1270px;position:absolute;left:-30px;top:42%;width:22px;height:22px"></a>')
                            imageDiv.append(sendAgain);
                            sendAgain.on('click', function () {
                                sendAgain.remove();
                                imageDiv.css('boder', 'none');
                                $(imageDiv.find('.img-loading')).show();
                                xhr.open("post", uploadUrl, true);
                                xhr.send(form);
                            })
                        }
                    }
                    xhr.send(form);
                }
                reader.readAsDataURL(fileObj);
            }
            document.getElementById("im_file").value = null;//上传成功,清空文件对象
        }
    };

    //移除无聊天记录文字
    _removeEmptyMessage = function () {
        $("#div_message_content_main >.empty").remove();
    }

    //渲染消息显示HTML
    var renderMessagesView = {
        text: {
            mine: function (message) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'ico-kefu.png' : 'sj_logo.png';
                var div = $('<div class="msg msg-right"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var text = $('<div class="text"><div class="arrow"></div><pre>' + message + '</pre></div>');
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(text).append(end);
            },
            others: function (username, message) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'sj_logo.png' : 'ico-kefu.png';
                var div = $('<div class="msg msg-left"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var text = $('<div><div class="username">' + username + '</div><div class="text"><div class="arrow"></div><pre>' + message + '</pre></div></div>');
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(text).append(end);
            }
        },
        image: {
            mine: function (options) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'ico-kefu.png' : 'sj_logo.png';
                var div = $('<div class="msg msg-right"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var content = $('<div class="pic"><img title=' + options.Attachment.AttachmentName + ' class="image_message"  src=' + options.AttachmentBase64 + ' data-image="../Attachment/GetImage?attachmentId=' + options.Attachment.ID + '" id="' + options.Attachment.ID + '"></div>');
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(content).append(end);
            },
            others: function (options) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'sj_logo.png' : 'ico-kefu.png';
                var div = $('<div class="msg msg-left"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var content = $('<div><div class="username">' + options.StaffName + '</div><div class="pic"><img title=' + options.Attachment.AttachmentName + ' class="image_message"  src=' + options.AttachmentBase64 + ' data-image="../Attachment/GetImage?attachmentId=' + options.Attachment.ID + '" id="' + options.Attachment.ID + '"></div></div>');
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(content).append(end);
            }
        },
        audio: {
            mine: function (id, time, attachmentId, isLocal) {
                _removeEmptyMessage();

                var sending = null;
                if (time)
                    sending = $('<p style="position:absolute;left:-30px;color:#000;font-family: 微软雅黑;">' + time + '\'\'</p>');
                else {
                    time = 0;
                    sending = $('<a id="sending" style="cursor:pointer;background: url(../Content/Images/hrc-loading.gif);position:absolute;left:-30px;;width:32px;height:32px;background-repeat:no-repeat;top:10px"></a>')
                }
                var head = 'sj_logo.png';
                var div = $('<div class="msg msg-right"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var content = $('<div class="audio" style="width:' + (50 + time * 10) + 'px" data-src="' +
                    (isLocal ? attachmentId : ('../Attachment/GetAudio?attachmentId=' + attachmentId)) +
                    '" data-duration="' + time + '"  id="' + id +
                    '"><div class="arrow"></div><div class="audio-source audio-source-right"></div></div>').append(sending);

                var end = $('<div style="clear:both"></div>');
                content.find('div').on('click', function () {
                    playAudio(content.attr('data-src'), content.find('.audio-source'), true);
                })
                return div.append(headImg).append(content).append(end);
            },
            others: function (time, attachmentId, username) {
                _removeEmptyMessage();

                var head = 'ico-kefu.png';
                var div = $('<div class="msg msg-left"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var content = $('<div><div class="username">' + username + '</div><div class="audio" style="width:' + (50 + time * 10) + 'px" data-src="../Attachment/GetAudio?attachmentId=' + attachmentId + '" data-duration="' + time + '"><div class="arrow"></div><div class="audio-source audio-source-left"></div><p style="position:absolute;right:-30px;color:#000;font-family: 微软雅黑;">' + time + '\'\'</p></div>');
                var end = $('<div style="clear:both"></div>');
                content.on('click', function () {
                    playAudio(content.attr('data-src'), content.find('.audio-source'), false);
                })
                return div.append(headImg).append(content).append(end);
            }
        },
        file: {
            mine: function (username, content) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'ico-kefu.png' : 'sj_logo.png';
                var div = $('<div class="msg msg-right"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(content).append(end);
            },
            others: function (username, content) {
                _removeEmptyMessage();

                var head = initViewOtions.customer ? 'sj_logo.png' : 'ico-kefu.png';
                var div = $('<div class="msg msg-left"></div>');
                var headImg = $('<i class="msg-head"><img src="../Content/Images/' + head + '" /></i>');
                var content_parent = $("<div></div>").append($('<div class="username">' + username + '</div>'))
                content_parent.append(content);
                var end = $('<div style="clear:both"></div>');
                return div.append(headImg).append(content_parent).append(end);
            }
        },
        time: function (time, top) {
            var main_massage = $('#div_message_content_main');
            var time_message = $('<div class="date">' + time + '</div>');
            if (top) {
                main_massage.prepend(time_message);
            }
            else {
                main_massage.append(time_message);
            }

            _lastTime = time;
        },
        scrollPosition: function () {
            var content = document.getElementById('chat');
            content.scrollTop = content.scrollHeight;
        },
    };

    //显示截图窗口
    var showNgdialog = function (blob) {
        var showImageDialog = $('<div class="ngdialog">' +
                            '<div class="ngdialog-content">' +
                                '<div class="dialog_hd">' +
                                    '<h3 class="title">发送图片<h3>' +
                                '</div>' +
                                '<div class="dialog_bd">' +
                                    '<img />' +
                                    '<span class="vm_box"></span>' +
                                '</div>' +
                                '<div class="dialog_ft">' +
                                    '<a class="btn btn_default" href="javascript:">取消</a>' +
                                    '<a class="btn btn_primary" href="javascript:">发送</a>' +
                                '</div>' +
                                '<div class="ngdialog-close"></div>' +
                            '</div>' +
                            '</div>' +
                        '</div>');
        var btn_close = showImageDialog.find('.ngdialog-close');
        var btn_send = showImageDialog.find('.btn_primary');
        var btn_cancel = showImageDialog.find('.btn_default');
        var dialog_content = showImageDialog.find('.ngdialog-content');
        var image = showImageDialog.find('img');

        var reader = new FileReader();
        if (blob != null) {
            reader.onload = function (e) {
                image.attr('src', e.currentTarget.result)
                $('body').append(showImageDialog);
                var clientHeight = parseInt($(window).height());
                var contentHeight = parseInt(dialog_content.height());
                if (clientHeight > contentHeight) {
                    dialog_content.css('top', (clientHeight - contentHeight) / 2);
                }
                btn_close.on('click', function (e) {
                    showImageDialog.remove();
                })
                btn_cancel.on('click', function (e) {
                    showImageDialog.remove();
                })
                btn_send.on('click', function (e) {
                    blob.name = "screenshot." + blob.type.split('/')[1];
                    send.sendFile(blob, _messageType.image);
                    showImageDialog.remove();
                })
            };
            reader.readAsDataURL(blob);
        }
    };

    //显示看图层
    var imageView = function (e) {
        var mask = $('<div id="mask_layer" style="width:100%;height:100%;position:fixed;background-color:rgba(0, 0, 0, 0.80);top:0px;text-align:center;z-index:99999">' +
                        '<div id="mask_layer_close" style="width:50px;height:50px;position:absolute;right:0px;background: url(../Content/Images/weChat.png) 0px -608px;cursor:pointer;z-index:999"></div>' +
                        '<img id="image_view" src="' + $(this).attr("data-image") + '" style="position:relative;display:none;max-width:1960px;max-height:1200px;min-width:50px;min-height:50px"/>' +
                        '<img id="image_loading" src="../Content/Images/hrc-loading.gif" style="width:50px;height:50px;position:relative;top:48%"/>' +
                        '<div id="image_bar"><ul class="image_bar_list"><li class="image_bar_item"><a id="image_bar_prev"><i class="image_bar_left"></i></li>' +
                        '<li class="image_bar_item"><a id="image_bar_download" href="' + initViewOtions.transport.download + '?attachmentId=' + $(this).attr('id') + '"><i class="image_bar_download"></i>' +
                        '<li class="image_bar_item"><a id="image_bar_rotate"><i class="image_bar_rotate"></i>' +
                        '<li class="image_bar_item"><a id="image_bar_next"><i class="image_bar_right"></i>' +
                        '</ul></div></div>');

        $('body', window.parent.document).append(mask);

        var image_view = mask.find('#image_view');
        var image_prev = mask.find('#image_bar_prev');
        var image_download = mask.find('#image_bar_download');
        var image_rotate = mask.find('#image_bar_rotate');
        var image_next = mask.find('#image_bar_next');
        var image = new Image();

        //获取所有页面上的图片链接
        var image_src = [], image_title = [], image_id = [];
        var images = $('#div_message_content_main .image_message', window.parent.document);
        $.each(images, function (key, val) {
            image_src.push($(val).attr('data-image'));
            image_title.push($(val).attr('title'));
            image_id.push($(val).attr('id'));
        });

        //大图加载
        image.src = $(this).attr("data-image")
        image.onload = function () {
            var top = (parseInt($(window).height()) - parseInt(image_view.height())) / 2
            image_view.css({ "top": top });
            mask.find('#image_loading').hide();
            image_view.show();
            var current = 0;
            //获取当前图片所在的位置
            var index = image_src.indexOf('../' + image.src.split('/')[3] + '/' + image.src.split('/')[4]);

            if (index == 0) {
                image_prev.find('i').removeClass('image_bar_left').addClass('image_bar_left_disable');
            }
            else if (index == image_src.length - 1) {
                image_next.find('i').removeClass('image_bar_right').addClass('image_bar_right_disable');
            }
            //图片加载完毕,加载点击事件
            //image_bar按钮事件绑定
            image_prev.on('click', function (e) {//上一张图
                if (index > 0) {
                    image_view.hide();
                    mask.find('#image_loading').show();
                    image.src = image_src[index - 1]
                    image_prev.find('i').removeClass('image_bar_left_disable').addClass('image_bar_left');
                    image_next.find('i').removeClass('image_bar_right_disable').addClass('image_bar_right');
                    image_view.attr('src', image_src[index - 1]).css({ 'height': '', 'width': '', 'transform': '', 'top': '', 'left': '' });
                    image_download.attr('href', initViewOtions.transport.download + '?attachmentId=' + image_id[index - 1]);
                    index--;
                }
            });
            image_rotate.on('click', function (e) {//旋转图片
                current = (current + 90) % 360;
                image_view[0].style.transform = 'rotate(' + current + 'deg)';
            });
            image_next.on('click', function (e) {//下一张图片
                if (index < image_src.length - 1) {
                    image_view.hide();
                    mask.find('#image_loading').show();
                    image.src = image_src[index + 1]
                    image_prev.find('i').removeClass('image_bar_left_disable').addClass('image_bar_left');
                    image_next.find('i').removeClass('image_bar_right_disable').addClass('image_bar_right');
                    image_view.attr('src', image_src[index + 1]).css({ 'height': '', 'width': '', 'transform': '', 'top': '', 'left': '' });
                    image_download.attr('href', initViewOtions.transport.download + '?attachmentId=' + image_id[index + 1]);
                    index++;
                }
            });
        }

        var move = false;
        var _x, _y;
        //图片移动事件
        image_view.on('mousedown', function (e) {
            if (e.button != 0) {
                return 0;
            }
            move = true;
            _x = e.pageX - parseInt(image_view.css("left"));
            _y = e.pageY - parseInt(image_view.css("top"));
        }).on('mousemove', function (e) {
            if (move) {
                var x = e.pageX - _x;//控件左上角到屏幕左上角的相对位置
                var y = e.pageY - _y;
                image_view.css({ "top": y, "left": x });
            }
        }).on('mouseup', function () {
            move = false;
        });
        //图片缩放事件
        if (mask[0].addEventListener) {
            mask[0].addEventListener('mousewheel', function (e) {
                if (e.wheelDelta) {//IE/Opera/Chrome 
                    var imgWidth = parseInt(image_view.css('width'));
                    var imgHeight = parseInt(image_view.css('height'));
                    var maxImgWidth = parseInt(image_view.css('max-width'));
                    var maxImgHeight = parseInt(image_view.css('max-height'));
                    var minImgWidth = parseInt(image_view.css('min-width'));
                    var minImgHeight = parseInt(image_view.css('min-height'));
                    if (e.wheelDelta == 120) { //向上滑动
                        if (imgWidth >= maxImgWidth || imgHeight >= maxImgHeight || imgWidth * 1.1 > maxImgWidth || imgHeight * 1.1 > maxImgHeight)
                            return;
                        image_view.css('width', imgWidth * 1.1).css('height', imgHeight * 1.1);

                    }
                    else { //向下滑动
                        if (imgWidth <= minImgWidth || imgHeight <= minImgHeight || imgWidth * 0.9 < minImgWidth || imgHeight * 0.9 < minImgHeight)
                            return;
                        image_view.css('width', imgWidth * 0.9).css('height', imgHeight * 0.9);
                    }
                } else if (e.detail) {//Firefox 
                    if (e.wheelDelta == 3) { //向上滑动
                        mg.css('width', imgWidth * 1.1).css('height', imgHeight * 1.1);
                    }
                    else {
                        image_view.css('width', imgWidth * 0.9).css('height', imgHeight * 0.9);
                    }
                }
                e.stopPropagation();
                e.preventDefault();
            });

        }
        //关闭按钮事件
        mask.find('#mask_layer_close').on('click', function () {
            $("#mask_layer", window.parent.document).fadeOut(function () {
                $(this).remove();
            });
        })
        //禁止拖曳图片,全浏览器兼容
        image_view[0].ondragstart = function () { return false; }
        e ? e.stopPropagation() : event.cancelBubble = true;
    };

    $(document).click(function (e) {
        $("#expressionDiv").fadeOut();
    });

    //播放语音
    var playAudio = function (url, audioEle, isMine) {
        $('.audio-source-left').css('background', 'url(../Content/Images/web_im_voice_left.png) no-repeat');
        $('.audio-source-right').css('background', 'url(../Content/Images/web_im_voice_right.png) no-repeat');
        $('body').find('audio').remove();

        audioEle.css('background', isMine ? 'url(../Content/Images/web_im_voice_playing_right.gif)' : 'url(../Content/Images/web_im_voice_playing_left.gif)')
        var audio = $('<audio style="display:none"></audio>');
        $('body').append(audio);
        audio.attr('src', url);
        //音频加载完毕后发生
        audio[0].addEventListener('canplaythrough', function (e) {
            audio[0].play();
        }, false);
        audio[0].addEventListener('ended', function () {
            audioEle.css('background', isMine ? 'url(../Content/Images/web_im_voice_right.png) no-repeat' : 'url(../Content/Images/web_im_voice_left.png) no-repeat')
            audio.remove();
        });
    };

    //客户端视图初始化
    var customerViewInit = function (e) {
        var expressionStr = '';
        //获取表情字符串
        $.ajax({
            url: initViewOtions.transport.expression,
            type: 'get',
            dataType: "json",
            async: false,
            success: function (data) {
                expressionArray = data;
            }
        });
        $.each(expressionArray, function (key, val) {
            expressionStr += '<a title=' + val.Title + ' style="background:url(' + val.Path + ')  center center no-repeat;"></a>'

        });
        //获取服务器时间
        $.ajax({
            url: initViewOtions.transport.time,
            type: 'GET',
            async: false,
            success: function (date) {
                time = date;
            }
        });

        var div = $('<div style="position:absolute;width:100%;height:79.5%" class="scroll-wrapper box_bd chat_bd scrollbar-dynamic">' +
                    '<div id="chat">' +
                        '<div id="div_message_content_main"></div>' +
                        '<span id="msg_end" style="overflow:hidden"></span>' +
                    '</div>' +
                '</div>' +
                '<div style="position:absolute;width:500px;height: 20%;" class="box_ft">' +
                    '<div class="toolbar">' +
                        '<a id="web_im_face" class="web_im_face" href="javascript:;" title="表情"></a>' +
                        '<a id="web_im_pic" class="web_im_pic" href="javascript:;" title="文件"><input type="file" id="im_file" style="display:none"/></a>' +
                        '<a id="web_im_audio" class="web_im_audio" href="javascript:;" title="语音" style="display:none"></a>' +
                        '<a id="web_im_screenshot" class="web_im_screenshot" title="截屏"></a>' +
                        '<div id="expressionDiv" class="scroll-wrapper exp_bd scrollbar-dynamic" style="position: absolute;bottom:150px;display:none">' +
                            '<div class="expression">' +
                                '<div class="exp_cont">' +
                                    '<div class="qq_face">' +
                                        expressionStr +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div id="div_message_send">' +
                        '<pre id="editArea" class="editArea" contenteditable="true"></pre>' +
                        '<div>' +
                            '<button id="btn_send" disabled="disabled" >发送</button>' +
                        '</div>' +
                        '<div style="clear:both;height:20px"></div>' +
                    '</div>' +
                '</div>');
        e.append(div);
        //截屏插件初始化;
        capturewrapperInit(function (type, x, y, width, height, info, content, localpath) {
            if (type < 0) {
                //type < 0 插件未能初始化,弹出指引教程
                var window = $('<div id="kendo_window"><p>提示信息:</p></div>');
                window.kendoWindow({
                    width: "600px",
                    title: "截屏插件加载失败",
                    visible: false,
                    actions: [
                        "Close"
                    ],
                }).data("kendoWindow").center().open();
                return;
            }
            if (type == 1) {
                var mimeString = 'image/jpg';
                var byteString = atob(content); //base64 解码
                var arrayBuffer = new ArrayBuffer(byteString.length); //创建缓冲数组
                var intArray = new Uint8Array(arrayBuffer); //创建视图
                for (i = 0; i < byteString.length; i += 1) {
                    intArray[i] = byteString.charCodeAt(i);
                }
                var blob = new Blob([intArray], { type: mimeString }); //转成blob
                showNgdialog(blob);
            }
        });
        //绑定事件
        var expressionClick = e.find('#web_im_face');
        var fileClick = e.find('#web_im_pic');
        var audioClick = e.find('#web_im_audio');
        var screenshotClick = e.find('#web_im_screenshot');
        var fileInputClick = e.find('#im_file');
        var expressionDiv = e.find('#expressionDiv');
        var iconClick = e.find('.qq_face a');
        var editArea = e.find('#editArea');
        var sendClick = e.find('#btn_send');
        var box_ft = e.find('.box_ft');
        var find_div = e.find('#div_historyMessage_find');
        var microphone = $('<div class="microphone"><div class="microphone_icon"></div><div class="microphone_text">向上滑取消发送</div></div>');
        var recorder;
        var mouseDown = false;
        //点击截屏菜单
        screenshotClick.on('click', function (e) {
            StartCapture();
        })
        //点击表情按钮事件绑定
        expressionClick.on('click', function (e) {
            var expression = $("#expressionDiv");
            if (expression.is(':hidden')) {
                expression.fadeIn();
                e ? e.stopPropagation() : event.cancelBubble = true;
            }
            else
                expression.fadeOut();
            return false;
        });
        //点击文件按钮事件绑定
        fileClick.on('click', function (e) {
            fileInputClick[0].click();
        });
        //语音事件绑定
        audioClick.on('mousedown', function (e) {
            mouseDown = true;
            $('body').append(microphone);
            microphone.fadeIn(function () {
                HZRecorder.get(function (rec) {
                    recorder = rec;
                    recorder.start();
                });
                var microphoneleft = microphone.find('.microphone_icon');
                var microphoneLeftfadeOut = function (e) {
                    microphoneleft.fadeOut(800, function (e) {
                        microphoneleft.fadeIn(800, function (e) {
                            if ($('body').find(microphone).length != 0) {
                                microphoneLeftfadeOut();
                            }
                        });

                    })
                }
                microphoneLeftfadeOut();
            });
        })
        audioClick.on('mouseleave', function () {
            mouseDown = false;
            microphone.remove();
        });
        audioClick.on('mouseup', function (e) {
            if (mouseDown) {
                mouseDown = false;
                endOfRecording();
            }

        })
        //录音结束后调用事件,鼠标弹起,离开,录音超时均调用此事件
        var endOfRecording = function () {
            mouseDown = false;
            microphone.fadeOut(function () {
                if (recorder === undefined) {
                    showTips("语音失败,请检查是否授权录音");
                }
                microphone.remove();
                var duration = 0;
                recorder.stop();
                var record = recorder.getBlob();
                var audioid = baseTool.newGuid();
                audioquery = $('<audio></audio>');
                audioquery[0].src = window.URL.createObjectURL(record)
                audioquery[0].addEventListener('canplaythrough', function (e) {
                    duration = Math.ceil(audioquery[0].duration);
                    var audio = renderMessagesView.audio.mine(audioid, null, window.URL.createObjectURL(record), true);
                    var main_massage = $('#div_message_content_main');
                    audio.find('.audio').css('width', 50 + duration * 10);
                    audio.find('.audio').attr('data-duration', duration);
                    main_massage.append(audio);
                    renderMessagesView.scrollPosition();
                    recorder.upload(initViewOtions.transport.upload, function (options, e, response) {
                        if (options == 'ok') {
                            try {
                                response = eval("(" + response + ")");
                                send.sendFileMessage(response.Item3[0].ID, audioid, _messageType.audio);
                            }
                            catch (ex) {
                                audio.find('#sending').css('background', 'url(../Content/Images/weChat.png) 0 -1270px');
                            }
                        }
                        else if (options == 'error' || options == 'cancel') {
                            audio.find('#sending').css('background', 'url(../Content/Images/weChat.png) 0 -1270px');
                            //recorder.upload(initViewOtions.transport.upload, uploadCallBack);
                        }
                    });
                    //var uploadCallBack = 
                }, false);
            });
        }
        //文件选择后事件绑定
        fileInputClick.on('change', function (e) {
            var fileObj = document.getElementById("im_file").files[0]; // 获取文件对象
            if (!$.isEmptyObject(fileObj)) {
                send.sendFile(fileObj, _messageType.file);
            }
        });
        //点击表情区域事件绑定
        expressionDiv.on('click', function (e) {
            e ? e.stopPropagation() : event.cancelBubble = true;
        });
        //表情选择事件绑定
        iconClick.on('click', function (e) {
            var url = $(this).css('backgroundImage');
            var title = $(this).attr('title');
            url = url.substring(4, url.length - 1);
            var text = editArea[0].innerHTML == undefined ? '' : editArea[0].innerHTML;
            editArea[0].innerHTML = text + '<img title=' + title + ' src=' + url + '/>';
            //expressionDiv.fadeOut();
            focusEnd(editArea[0]);
        });
        //发送按钮事件绑定
        sendClick.on('click', function () {
            if (editArea.innerHTML != '') {
                send.sendTextMessage(editArea);
            }
        });

        //绑定粘贴事件,仅粘贴图片
        $(editArea).bind({
            paste: function (e) {//paste事件  
                var items = (event.clipboardData || event.originalEvent.clipboardData).items;
                var index = -1;
                $.each(items, function (key, val) {
                    if (val.type.indexOf('image') > -1) {
                        index = key;
                    }
                })
                if (index > -1) {
                    var blob = items[index].getAsFile();
                    if (blob != null) {
                        showNgdialog(blob);
                    }
                }
            }
        });
    };

    //对外暴露接口
    $.fn.initView = function (otions) {
        $.extend(initViewOtions, otions);
        this_div = $(this);
        $("body").on('click', ".image_message", imageView);
        _chatArea = this;
        _chatArea.css('height', initViewOtions.height + 'px');
        customerViewInit(this);


        if (!initViewOtions.signalR.enable) {
            $('.box_ft').remove();
            $('.box_bd').css('height', '100%')
        } else {
            initSignalR();
        }
        getMessage();
    }

    //初始化页面
    initMessageView = function (e) {

        var selectedRows = e.select();

        _selectedStaffId = selectedRows.find('table').eq(0).attr('id');

        _selectedStaffName = selectedRows.find('table').eq(0).attr('name');

        //查找当前点击员工消息是否存在,如果存在则直接初始化界面
        var message = messageDates.singleOrDefault(function (obj) { return obj.StaffId == _selectedStaffId });

        if ($.isEmptyObject(message) || message.length == 0 || message.Messages.length == 0) {
            recentContactResult = recentContactList.singleOrDefault(function (obj) { return obj.StaffId == _selectedStaffId });

            if (!$.isEmptyObject(recentContactResult)) {
                var page = Math.ceil(recentContactResult.UnReadMessagesCount / initViewOtions.pageSize);
                page = page == 0 ? page + 1 : page;
                for (var i = 0; i < page; i++) {
                    readMessage(_selectedStaffId);
                }
                message = messageDates.singleOrDefault(function (obj) { return obj.StaffId == _selectedStaffId });
            }
        }
        else {
            var data = { 'ConsultationQuestionIds': [] };
            $.each(message.Messages, function (key, val) {
                data.ConsultationQuestionIds.push(val.ID);
            });
            $.ajax({
                url: initViewOtions.transport.read,
                data: data,
                type: 'POST'
            })
        }
        //判断对话窗口是否是隐藏的,是则显示
        if (_chatArea.is(':hidden')) {
            _chatArea.show();
        }

        //判断是否在加载历史记录
        if ($('.more-msg >a').is(':hidden')) {
            $('.more-msg >img').attr('src', '../../Content/Images/icon-time.png');
            $('.more-msg >a').css({ 'display': 'inline' })
        }

        //导航显示名称
        $('#txt_fullName').text(_selectedStaffName);

        //移除所有对话记录
        $('#div_message_content_main').empty();

        //遍历联系人管理器,去除未读消息角标
        $.each(recentContactList, function (index, val) {
            if (val.StaffId == _selectedStaffId) {
                if (val.UnReadMessagesCount == 0) {
                    return;
                }
                //将未读消息标记为0
                val.UnReadMessagesCount = 0;
            }
        })
        $.each(message.Messages, function (key, val) {
            var lastTime = key == 0 ? null : message.Messages[key - 1].SendTime;
            showMessageView(val, lastTime);
        });
        renderMessagesView.scrollPosition();
    };

    var showMessageView = function (options) {
        var main_massage = $('#div_message_content_main');
        renderMessagesView.time(options.CreatedDate);
        if (options.SendBy == 'Mine') {
            switch (options.MessageType) {
                case _messageType.text:
                    message = renderMessagesView.text.mine(options.CommentText.toHtml());
                    break;
                case _messageType.image:
                    message = renderMessagesView.image.mine(options);
                    break;
                case _messageType.file:
                    if (options.CommentText === null) {
                        options.CommentText = $('<div class="file" id="' + options.Attachment.ID + '">' +
                        '<div class="arrow"></div>' +
                        '<div class="file_bd">' +
                            '<div class="cover">' +
                                '<i></i>' +
                            '</div>' +
                            '<div class="cont">' +
                                '<p>' + options.Attachment.AttachmentName + '</p>' +
                                '<div class="opr">' +
                                    '<span>' + (options.Attachment.AttachmentSize / 1024 / 1024).toFixed(2) + 'M</span>' +
                                    '<span>|</span>' +
                                    '<a href="../Attachment/DownloadAttachment?attachmentId=' + options.Attachment.ID + '">下载</a>' +
                                '</div>' +
                            '</div>' +
                        '<div>' +
                    '</div>');
                    }
                    message = renderMessagesView.file.mine('', options.CommentText);
                    break;
                case _messageType.audio:
                    message = renderMessagesView.audio.mine('', options.Attachment.PlayTime, options.Attachment.ID, false);
                    break;
            }
        }
        else {
            switch (options.MessageType) {
                case _messageType.text:
                    message = renderMessagesView.text.others(options.StaffName, options.CommentText.toHtml());
                    break;
                case _messageType.image:
                    message = renderMessagesView.image.others(options);
                    break;
                case _messageType.file:
                    if (options.CommentText === null) {
                        options.CommentText = $('<div class="file" id="' + options.Attachment.ID + '">' +
                        '<div class="arrow"></div>' +
                        '<div class="file_bd">' +
                            '<div class="cover">' +
                                '<i></i>' +
                            '</div>' +
                            '<div class="cont">' +
                                '<p>' + options.Attachment.AttachmentName + '</p>' +
                                '<div class="opr">' +
                                    '<span>' + (options.Attachment.AttachmentSize / 1024 / 1024).toFixed(2) + 'M</span>' +
                                    '<span>|</span>' +
                                    '<a href="../Attachment/DownloadAttachment?attachmentId=' + options.Attachment.ID + '">下载</a>' +
                                '</div>' +
                            '</div>' +
                        '<div>' +
                    '</div>');
                    }
                    message = renderMessagesView.file.others(options.StaffName, options.CommentText);
                    break;
                case _messageType.audio:
                    message = renderMessagesView.audio.others(options.Attachment.PlayTime, options.Attachment.ID, options.StaffName);
                    break;
            }
        }
        main_massage.append(message);
        renderMessagesView.scrollPosition();
    };

    var showTips = function (tipMssages) {
        var tip = $('<div id="msgTips"></div>');
        $('body').append(tip);
        tip[0].innerText = tipMssages;
        $('#msgTips').fadeIn();
        setTimeout(function () {
            tip.fadeOut(function () {
                tip.remove();
            });
        }, 3000);
    };

    var focusEnd = function (el) {
        $(el).focus();
        if ($.support.msie) {
            var rng;
            el.focus();
            rng = document.selection.createRange();
            rng.moveStart('character', -el.innerText.length);
            var text = rng.text;
            for (var i = 0; i < el.innerText.length; i++) {
                if (el.innerText.substring(0, i + 1) == text.substring(text.length - i - 1, text.length)) {
                    result = i + 1;
                }
            }
        }
        else {
            var range = document.createRange();
            range.selectNodeContents(el);
            range.collapse(false);
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        }
        //将输入框的滚动条置于最下面
        $('#editArea').scrollTop($('#editArea')[0].scrollHeight);
    };

    //事件监听
    $(function () {
        var area = $('#chatArea')[0];
        document.addEventListener("dragenter", function (e) {
            e.stopPropagation();
            e.preventDefault();
            e.dataTransfer.dropEffect = "none";
            e.dataTransfer.effectAllowed = "none";
        }, false);
        document.addEventListener("dragover", function (e) {
            e.stopPropagation();
            e.preventDefault();
            e.dataTransfer.dropEffect = "none";
            e.dataTransfer.effectAllowed = "none";
        }, false);
        document.addEventListener("drop", function (e) {
            e.stopPropagation();
            e.preventDefault();
            e.dataTransfer.dropEffect = "none";
            e.dataTransfer.effectAllowed = "none";
        }, false);
        area.addEventListener("dragenter", function (e) {
            e.stopPropagation();
            e.preventDefault();
        }, false);
        area.addEventListener("dragover", function (e) {
            e.stopPropagation();
            e.preventDefault();
        }, false);
        area.addEventListener("drop", function (e) {
            e.stopPropagation();
            e.preventDefault();
            $.each(e.dataTransfer.files, function (key, val) {
                send.sendFile(val, _messageType.file);
            });
        }, false);
        document.addEventListener("keydown", function (e) {
            var oEvent = e || window.event;
            if (oEvent.keyCode == 13 && oEvent.ctrlKey) {
                var editArea = $('#editArea')[0]
                var message = editArea.innerHTML;
                message += '\n\n';
                editArea.innerHTML = message;
                focusEnd(editArea);
            }
            else if (oEvent.keyCode == 13) {
                $('#btn_send').trigger("click");
                event.stopPropagation();
                event.preventDefault();
                return false;
            }

            else if (oEvent.keyCode == 8) {
                var editArea = $('#editArea')[0];
                if (editArea.innerHTML == '&#10;')
                    editArea.innerHTML = '';
                return oEvent;
            }
            else if (oEvent.keyCode == 27) {
                $("#mask_layer", window.document.parentWindow).fadeOut(function () {
                    $(this).remove();
                });
            }
        }, false);
    })

})(jQuery)