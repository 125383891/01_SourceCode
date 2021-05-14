/***
Wrapper/Helper Class for datagrid based on jQuery Datatable Plugin
***/
var Datatable = function () {

    var tableOptions; // main options
    var dataTable; // datatable object
    var table; // actual table jquery object
    var tableContainer; // actual table container object
    var tableWrapper; // actual table wrapper jquery object
    var tableInitialized = false;
    var ajaxParams = {}; // set filter mode
    var the;

    var countSelectedRecords = function () {
        var selected = $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).size();
        var text = tableOptions.dataTable.language.metronicGroupActions;
        if (selected > 0) {
            $('.table-group-actions > span', tableWrapper).text(text.replace("_TOTAL_", selected));
        } else {
            $('.table-group-actions > span', tableWrapper).text("");
        }
    };

    //reset header checkbox
    var resetHeaderCheckbox = function () {
        var checkedHeaderCheckbox = $('thead > tr > th:nth-child(1) input[type="checkbox"]:checked', table);
        if (checkedHeaderCheckbox.size() > 0) {
            $(checkedHeaderCheckbox[0]).prop('checked', false);
            //$(checkedHeaderCheckbox[0]).uniform();
        }
    }

    var rowSelect = function (rowClickCallback) {
        //事件绑定在td上
        $('tbody', table).off('click').on('click', 'td', function (event) {
            //console.log(event);
            //event.stopPropagation();
            $thisTr = $(this).parents('tr').eq(0);
            if (event.target.tagName == 'TD' || event.target.tagName == 'DIV') {
                var $thisCheckbox = $("td:nth-child(1) input[type='checkbox']", $thisTr);
                if ($thisCheckbox.length > 0) {
                    if ($thisCheckbox.is(':checked')) {
                        $thisCheckbox.prop('checked', false);
                    } else {
                        $thisCheckbox.prop('checked', true);
                    }
                    $thisCheckbox.change();
                    //$thisCheckbox.uniform();
                }


                var $thisRadio = $("td:nth-child(1) input[type='radio']", $thisTr);
                if ($thisRadio.length > 0) {

                    var radioName = $thisRadio.attr('name');
                    $("input[name=" + radioName + "]", table).prop('checked', false);
                    $thisRadio.prop('checked', true);

                   
                    //$("input[name=" + radioName + "]", table).uniform();
                    $thisRadio.change();
                }


            }

            if (event.target.tagName == 'TD' || event.target.tagName == 'INPUT' || event.target.tagName == 'DIV') {

                if (rowClickCallback && typeof rowClickCallback == 'function') {
                    rowClickCallback($thisTr);
                }
            }

        });
    }

    var addChildRow = function (addCallback) {
        $('tbody', table).on('click', '.btn-collapse', function (event) {
            var _this = $(this);
            var colums = tableOptions.dataTable.columns;
            if (!_this.data('expand')) {
                if (_this.data('isload')) {
                    var childRows = _this.parents('tbody').find('tr[data-parent="' + _this.data('parent') + '"]');
                    childRows.show();
                } else {
                    if (addCallback && typeof addCallback == 'function') {
                        var currentRowData = dataTable.row(_this.parents("tr")).data() || _this.parents("tr").data('data');
                        //if (!currentRowData) {
                        //    currentRowData = _this.parents("tr").data('data');
                        //}
                        var parentLv = _this.parents('tr').data('lv');
                        var treelevel = parentLv ? parentLv + 1 : 1;

                        addCallback(currentRowData, function (data, paddingColIndex) {

                            for (var i = 0; i < data.length; i++) {
                                var dataItem = data[i];
                                data[i].treelevel = treelevel;
                                var bgColor = getNextColor('#eeeeff', 0.2 * (treelevel - 1));

                                var trhtml = "<tr style='background:" + bgColor + ";' role='row' data-sysno='" + dataItem.sysno + "' data-parent='" + currentRowData.sysno + "' data-child='1'>";
                                for (var j = 0; j < colums.length; j++) {
                                    var column = colums[j];
                                    var tdcon = "";
                                    if (column.render && typeof column.render == "function") {
                                        tdcon = column.render(dataItem[column.data], "display", dataItem);
                                    } else {
                                        tdcon = dataItem[column.data];
                                    }

                                    trhtml += "<td>" + tdcon + "</td>";
                                }
                                trhtml += "</tr>";


                                var dom = $(trhtml);

                                if (paddingColIndex !== undefined) {
                                    var paddingTd = $('td:eq(' + paddingColIndex + ')', dom);
                                    paddingTd.css('padding-left', (25 * treelevel) + 'px');

                                    var _expandTd = $('td:eq(' + (paddingColIndex - 1) + ')', dom).css('position', 'relative');
                                    _expandTd.find('a.btn-collapse').css({
                                        'position': 'absolute',
                                        'top': _this.parents('tr').height() / 2 - 8 + 'px',
                                        'left': 25 * treelevel + 'px'
                                    });
                                }

                                dom.data('data', dataItem);
                                dom.data('lv', treelevel);
                                _this.parents('tr').after(dom);

                            }
                            _this.data('isload', true)
                        });
                    }
                }
                _this.html('<i class="fa fa-minus-square-o"></i>');
                _this.data('expand', true);
            } else {
                var childRows = _this.parents('tbody').find('tr[data-parent="' + _this.data('parent') + '"]');
                childRows.hide();
                var expandChildren = $('.btn-collapse', childRows).filter(function () {
                    return $(this).data('expand') == true;
                });
                expandChildren.click();

                _this.html('<i class="fa fa-plus-square-o"></i>');
                _this.data('expand', false);
            }
        });
    }
    var getNextColor = function (color, treelevel) {
        this.getColor = function (c, l) {
            var r = /^\#?[0-9a-f]{6}$/;
            if (!r.test(c)) return;
            var rgbc = this.HexToRgb(c);
            for (var i = 0; i < 3; i++) rgbc[i] = Math.floor((255 - rgbc[i]) * l + rgbc[i]);
            return this.RgbToHex(rgbc[0], rgbc[1], rgbc[2]);
        }
        this.HexToRgb = function (str) {
            str = str.replace("#", "");
            var hxs = str.match(/../g);
            for (var i = 0; i < 3; i++) hxs[i] = parseInt(hxs[i], 16);
            return hxs;
        }
        this.RgbToHex = function (a, b, c) {
            var r = /^\d{1,3}$/;
            if (!r.test(a) || !r.test(b) || !r.test(c)) return;
            var hexs = [a.toString(16), b.toString(16), c.toString(16)];
            for (var i = 0; i < 3; i++) if (hexs[i].length == 1) hexs[i] = "0" + hexs[i];
            return "#" + hexs.join("");
        }
        return this.getColor(color, treelevel)
    }

    return {

        //main function to initiate the module
        init: function (options) {

            if (!$().dataTable) {
                return;
            }

            the = this;

          
            var userFnCallback;
            if (options && options.dataTable) {
                userFnCallback = options.dataTable.drawCallback;
                delete options.dataTable.drawCallback;
            }
            
            
            // default settings
            options = $.extend(true, {
                src: "", // actual table  
                filterApplyAction: "filter",
                filterCancelAction: "filter_cancel",
                resetGroupActionInputOnSuccess: true,
                loadingMessage: 'Loading...',
                tdSelect:true,
                dataTable: {
                    "dom": "t<'row DTTTFooter'<'col-sm-6'lis><'col-sm-6'p>>", // datatable layout
                    "lengthMenu": [10, 25, 50, 100, 500, 1000],
                    "pageLength": 10, // default records per page
                    "language": { // language settings
                        // metronic spesific
                        "metronicGroupActions": "_TOTAL_ records selected:  ",
                        "metronicAjaxRequestGeneralError": "Could not complete request. Please check your internet connection",

                        //// data tables spesific
                        "lengthMenu": "<span class='seperator'></span> _MENU_ ",
                        "info": "<span class='seperator'></span>共 _TOTAL_ 条记录",
                        "infoEmpty": "没有找到可用数据",
                        "emptyTable": "<div class='tc'>暂时没有数据</div>",
                        "zeroRecords": "没有找到匹配的数据",
                        "paginate": {
                            "previous": "上一页",
                            "next": "下一页",
                            "last": "最后一页",
                            "first": "第一页",
                            "page": "",
                            "pageOf": ""
                        }
                    },

                    "orderCellsTop": true,
                    "columnDefs": [{ // define columns sorting options(by default all columns are sortable extept the first checkbox column)
                        'orderable': false,
                        'targets': [0]
                    }],

                    "pagingType": "bootstrap_full_number", // pagination type(bootstrap, bootstrap_full_number or bootstrap_extended)
                    "autoWidth": false, // disable fixed width and enable fluid table
                    "processing": false, // enable/disable display message box on record load
                    "serverSide": true, // enable/disable server side ajax loading

                    "ajax": { // define ajax settings
                        "url": "", // ajax URL
                        "type": "POST", // request type
                        "timeout": 20000,
                        "data": function (data) { // add request parameters before submit
                            $.each(ajaxParams, function (key, value) {
                                data[key] = value;
                            });
                            //App.blockUI({
                            //    message: tableOptions.loadingMessage,
                            //    target: tableContainer,
                            //    overlayColor: 'none',
                            //    cenrerY: true,
                            //    boxed: true
                            //});
                        },
                        "dataSrc": function (res) { // Manipulate the data returned from the server
                            if (res.Success === false) {
                                //ajax出错时，返回空数据
                                res.iTotalRecords = 0;
                                res.iTotalDisplayRecords = 0;
                                return [];
                            }
                            //if (res.customActionMessage) {
                            //    App.alert({
                            //        type: (res.customActionStatus == 'OK' ? 'success' : 'danger'),
                            //        icon: (res.customActionStatus == 'OK' ? 'check' : 'warning'),
                            //        message: res.customActionMessage,
                            //        container: tableWrapper,
                            //        place: 'prepend'
                            //    });
                            //}

                            if (res.customActionStatus) {
                                if (tableOptions.resetGroupActionInputOnSuccess) {
                                    $('.table-group-action-input', tableWrapper).val("");
                                }
                            }

                            //if ($('thead > tr > th:nth-child(1) input[type="checkbox"]', table).size() === 1) {
                            //    $('thead > tr > th:nth-child(1) input[type="checkbox"]', table).attr("checked", false);
                            //    //$.uniform.update($('thead > tr > th:nth-child(1) input[type="checkbox"]', table));
                            //}

                            if (tableOptions.onSuccess) {
                                tableOptions.onSuccess.call(undefined, the, res);
                            }

                            //App.unblockUI(tableContainer);

                            return res.aaData;
                        },
                        "error": function () { // handle general connection errors
                            if (tableOptions.onError) {
                                tableOptions.onError.call(undefined, the);
                            }

                            //App.alert({
                            //    type: 'danger',
                            //    icon: 'warning',
                            //    message: tableOptions.dataTable.language.metronicAjaxRequestGeneralError,
                            //    container: tableWrapper,
                            //    place: 'prepend'
                            //});

                            //App.unblockUI(tableContainer);
                        }
                    },

                    "drawCallback": function (oSettings) { // run some code on table redraw
                        if (tableInitialized === false) { // check if table has been initialized
                            tableInitialized = true; // set table initialized
                            table.show(); // display table
                        }
                        if (tableOptions.tdSelect) {
                            rowSelect(tableOptions.rowClickCallback);
                        }

                        if (tableOptions.tdCollapse) {
                            addChildRow(tableOptions.addChildRowData);
                        }

                        //App.
                        //initUniform($('input[type="checkbox"],input[type="radio"]', table)); // reinitialize uniform checkboxes on each table reload
                        countSelectedRecords(); // reset selected records indicator

                        // callback for ajax data load
                        if (tableOptions.onDataLoad) {
                            tableOptions.onDataLoad.call(undefined, the);
                        }

  
                        var self = this;

                        resetHeaderCheckbox();
                        if (userFnCallback) {
                            userFnCallback.call(self);
                        }
                    }
                }
            }, options);

            tableOptions = options;

            //dataTable.prototype.select = function (callback) {
                
            //}

            // create table's jquery object
            table = $(options.src);
            tableContainer = table.parents(".table-container");

            // apply the special class that used to restyle the default datatable
            var tmp = $.fn.dataTableExt.oStdClasses;

            $.fn.dataTableExt.oStdClasses.sWrapper = $.fn.dataTableExt.oStdClasses.sWrapper + " dataTables_extended_wrapper";
            $.fn.dataTableExt.oStdClasses.sFilterInput = "form-control input-xs input-sm input-inline";
            $.fn.dataTableExt.oStdClasses.sLengthSelect = "form-control input-xs input-sm input-inline";

            // initialize a datatable
            dataTable = table.DataTable(options.dataTable);

            // revert back to default
            $.fn.dataTableExt.oStdClasses.sWrapper = tmp.sWrapper;
            $.fn.dataTableExt.oStdClasses.sFilterInput = tmp.sFilterInput;
            $.fn.dataTableExt.oStdClasses.sLengthSelect = tmp.sLengthSelect;

            // get table wrapper
            tableWrapper = table.parents('.dataTables_wrapper');

            // build table group actions panel
            if ($('.table-actions-wrapper', tableContainer).size() === 1) {
                $('.table-group-actions', tableWrapper).html($('.table-actions-wrapper', tableContainer).html()); // place the panel inside the wrapper
                $('.table-actions-wrapper', tableContainer).remove(); // remove the template container
            }
            // handle group checkboxes check/uncheck
            table.find("thead > tr > th:nth-child(1) input[type='checkbox']").click(function () {
                var $tr = $('tbody > tr:has(:checkbox)', table);
                var checked = $(this).get(0).checked;
                if (checked) {
                    $tr.each(function (idx, obj) {
                        var disabled = $(this).find("td:nth-child(1) input[type='checkbox']").prop('disabled');
                        !disabled && $(this).addClass("selected");
                    });
                }
                else {
                    $tr.removeClass("selected");
                }
            });
            $('thead > tr > th:nth-child(1) input[type="checkbox"]', table).change(function () {
                var set = table.find('tbody > tr > td:nth-child(1) input[type="checkbox"]:not(":disabled")');
                var checked = $(this).prop("checked");
                $(set).each(function () {
                    $(this).prop("checked", checked);
                });
                //$.uniform.update(set);
                countSelectedRecords();
            });

            // handle row's checkbox click
            table.on('change', 'tbody > tr > td:nth-child(1) input[type="checkbox"]', function () {
                var checked = $(this).prop("checked");
                var $thisThead = $('thead > tr th:nth-child(1) input[type=checkbox]', table);
                var $checkbox = $('tbody > tr > td:nth-child(1) input[type="checkbox"]', table);
                if (checked) {
                    $(this).parents('tr').addClass("selected");
                    var allcheck = true;
                    $checkbox.each(function (idx,obj) {
                        !obj.checked && (allcheck = false);
                    });
                    allcheck && $thisThead.prop('checked', true);
                    //allcheck && $thisThead.prop('checked', true).uniform();
                } else {
                    $(this).parents('tr').removeClass("selected");
                    $thisThead.prop('checked', false)
                    //$thisThead.prop('checked', false).uniform();
                }
                countSelectedRecords();
            });

            // handle row's radio click
            table.on('change', 'tbody > tr > td:nth-child(1) input[type="radio"]', function () {
                var set = table.find('tbody > tr');
                set.removeClass("selected");
                $(this).parents('tr').addClass("selected");
                countSelectedRecords();
            });
            // handle filter submit button click
            table.on('click', '.filter-submit', function (e) {
                e.preventDefault();
                the.submitFilter();
            });

            // handle filter cancel button click
            table.on('click', '.filter-cancel', function (e) {
                e.preventDefault();
                the.resetFilter();
            });

            //tr click
            //table.on('click', 'tbody tr', function (e) {
            //    var checked = $(this).hasClass("selected");
            //    var set = $(this).find("td:nth-child(1) input[type='radio'], td:nth-child(1) input[type='checkbox']").prop("checked", checked);
            //    $.uniform.update(set);
            //});
        },

        submitFilter: function () {
            the.setAjaxParam("action", tableOptions.filterApplyAction);

            // get all typeable inputs
            $('textarea.form-filter, select.form-filter, input.form-filter:not([type="radio"],[type="checkbox"])', table).each(function () {
                the.setAjaxParam($(this).attr("name"), $(this).val());
            });

            // get all checkboxes
            $('input.form-filter[type="checkbox"]:checked', table).each(function () {
                the.addAjaxParam($(this).attr("name"), $(this).val());
            });

            // get all radio buttons
            $('input.form-filter[type="radio"]:checked', table).each(function () {
                the.setAjaxParam($(this).attr("name"), $(this).val());
            });

            dataTable.ajax.reload();
        },

        resetFilter: function () {
            $('textarea.form-filter, select.form-filter, input.form-filter', table).each(function () {
                $(this).val("");
            });
            $('input.form-filter[type="checkbox"]', table).each(function () {
                $(this).attr("checked", false);
            });
            the.clearAjaxParams();
            the.addAjaxParam("action", tableOptions.filterCancelAction);
            dataTable.ajax.reload();
        },

        getSelectedRowsCount: function () {
            return $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).size();
        },

        getSelectedRows: function () {
            var rows = [];
            $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).each(function () {
                rows.push($(this).val());
            });

            return rows;
        },

        setAjaxParam: function (name, value) {
            ajaxParams[name] = value;
        },

        addAjaxParam: function (name, value) {
            if (!ajaxParams[name]) {
                ajaxParams[name] = [];
            }

            skip = false;
            for (var i = 0; i < (ajaxParams[name]).length; i++) { // check for duplicates
                if (ajaxParams[name][i] === value) {
                    skip = true;
                }
            }

            if (skip === false) {
                ajaxParams[name].push(value);
            }
        },

        clearAjaxParams: function () {
            ajaxParams = {};
        },

        getDataTable: function () {
            return dataTable;
        },

        getTableWrapper: function () {
            return tableWrapper;
        },

        gettableContainer: function () {
            return tableContainer;
        },

        getTable: function () {
            return table;
        },
        reload: function () {
            dataTable.ajax.reload(null, true);
        },
        refresh: function () {
            dataTable.ajax.reload(null, false);
        },
        getSelectedRowsData: function (isRadio) {
            var rows = [];
            if (isRadio === false) {//单选
                $('tbody > tr.selected', table).each(function () {
                    rows.push(dataTable.row($(this)).data());
                });

            }
            else {//多选
                $('tbody > tr.selected', table).each(function () {
                    rows.push(dataTable.row($(this)).data());
                });

            }
            return rows;

        },
        deleteSelectedRows: function () {
            dataTable.row($('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').parents('tr')).remove().draw(false);
        },
        addRowData: function (data) {
            dataTable.rows.add(data).draw();
        },
        select: function () {
            table.DataTable().rows().select();
        },
        deselect: function () {
            table.DataTable().rows().deselect();
        },
        //获取当前记录数
        getRowsCount: function () {
            return this.getDataTable().context[0]._iRecordsTotal;
        },
        getRowData: function (element) {
            var tr = $(element).closest("tr");
            return dataTable.row(tr).data();
        },
        setSortNeutral: function () {
            table.dataTable().fnSetSortNeutral();
        }
    };
};

$.fn.dataTableExt.oApi.fnSetSortNeutral = function (oSettings) {
    /* Remove any current sorting by adding for example default sorting or leave it empty */
    oSettings.aaSorting = [];
    /* Redraw */
    //oSettings.oApi._fnReDraw(oSettings);
};