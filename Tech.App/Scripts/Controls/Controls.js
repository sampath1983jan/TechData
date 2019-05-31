var defaultDate = "00/00/0001T00:00:00";
// input controls
(function ($) {
    $.input = {
        defaults: {
            id: -1,
            isRequired: false,
            note: "",
            databind: "",
            enabletooltip: true,
            limit: true,
            min: 0,
            max: 100,
            name:"rd",
            regexp: "",
            textType: 1,    //1-text 2-password
            inputType: 1,    //1-text,2-number,3-decimal,4-alphaNumber,5-timeformat,6-email, //7-datepicker, 9-datetimepicker, 10-checkbox, 11-radio, 12-fileupload,
            //13-number range, 14.url,  15 color picker ,16 - yes/no,17 autextbox,19-combobox                           
            errorStyle: '',
            timeformat: 12,  //12,24  
            text: "",
            dateformat: "dd/mm/yyyy",   // dd/mm/yyyy,mm/dd/yyyy, dd-mm-yyyy,mm-dd-yyyy, dd mmm yyyy,dd mmmm yyyy,dd/mm/yy,mm/dd/yy, dd-mm-yy,mm-dd-yy,dd.mm.yyyy,mm.dd.yyyy,
            stephours: 1,
            stepmin: 10,
            overflowtime: false,
            datepickerType: 1,     // 1- single selection date picker,2 weekpicker,3-range picker 4-yearmonth picker
            range: false,
            precision: 2,
            orientation: 'horizontal',
            enable: true,
            selectPicker: {
                datasource: [],
                valueField: "",
                displayField: "",
                groupField:"",
                onSelected: function () {

                },
                container: "body",
                dropupAuto: true,
                width: 'auto',
                noneSelectedText: "Nothing Selected",
                noneResultsText: "No result found",
                selection: 'single', // single,multiple'
            },
            autofill: {
                type: 'select',  //select ,remote
                dataSource: [],
                getSource: function (searchval, action) {

                },
                valueField: "",
                displayField: "",
                sortable: true,
                placeholder: 'Type something to start...',
                allowCustomText: false,
                searchFromStart: false,
            },
            color: {
                format: 'rgb',
                defaultcolor: "transparent",
                onSelected: function (v) {

                },
                colorSelectors: {
                    'black': '#000000',
                    'white': '#ffffff',
                    'red': '#FF0000',
                    'default': '#777777',
                    'primary': '#337ab7',
                    'success': '#5cb85c',
                    'info': '#5bc0de',
                    'warning': '#f0ad4e',
                    'danger': '#d9534f'
                }

            },
            upload: {
                fileExtension: ["doc", "xlsx"],
                url: "",
                chuckSize: 100,
                param: "",
                maxSize: 5000,
                files:[],
                selection: 'single',  // single , multiple;
                showPreview: false,
                success: function () { },
                progress: function () { },
                error: function () { }
            },
            date: {
                datepickerType: 1,  // 1- single selection date picker,2 weekpicker,3-range picker 4-yearmonth picker
                dateformat: "dd/mm/yyyy", // dd/mm/yyyy,mm/dd/yyyy, dd-mm-yyyy,mm-dd-yyyy, dd mmm yyyy,dd mmmm yyyy,
                timeformat: 12 , //12,24  
                startDate: "",
                endDate: "",
                datetimeformat: "MM/DD/YYYY hh:mm A"
            }
        }
    };
    var methods = {
        init: function (options) {      
            var self = this;
            var prt = $.extend(true, {}, $.input.defaults, options);
            var tmp = methods._getTemplate(prt);
            methods._buildControl(tmp, prt, self);
            self.data('c_textbox', $.extend({}, prt, { initialized: true, waiting: false }));
            prt.parentID = self.id;
            $(this).find("input").blur(function () {
                $(self).find("[data-toggle=tooltip]").attr("title", "");
                $(self).find("[data-toggle=tooltip]").attr("data-original-title", "");
			});
			$(this).find("input").keypress(function () {              
                 return methods._isValid.call(self);                                       
            });
           // if (methods._isNeedKeyValidation(prt)) {
                methods._setRegExp(prt, self);                      
           // }            
            return this;
        },
        toggle: function () {
            var prt = this.data('c_textbox');
            if (this.find('input').prop('disabled')) {
                this.find('input').removeAttr('disabled');
            } else {
                this.find('input').attr('disabled','disabled');
            }

        },
        _isNeedKeyValidation: function (prt) {
            if (prt.inputType === 5) {
                return false;
            } else if (prt.inputType === 1)
                return true;
            else if (prt.inputType === 2)
                return true;
            else if (prt.inputType === 3) {
                return false;
            }
                return false;
        },
        _buildControl: function (tmp, prt,self) {
            if (prt.inputType === 5) {
                $(self).append(tmp);
                var sm = true;
                var oflow = prt.overflowtime;
                if (prt.date.timeformat !== 12) {
                    sm = false;
                    oflow = true;
                }
                $(self).find("input").timepicki({
                    show_meridian: sm,
                    min_hour_value: prt.min,
                    max_hour_value: prt.max,
                    step_size_minutes: prt.stepmin,
                    overflow_minutes: oflow,
                    increase_direction: 'down',
                    disable_keyboard_mobile: false,
                    start_time:prt.text
                });
            } else if (prt.inputType === 7 || prt.inputType === 9) {
                $(self).append(tmp);
                var ewr = false;
                var sdp = false;
                var rng = false;
                var ranges = {};
                var isdatetime = false;
                if (prt.inputType === 9) {
                    isdatetime = true;
                }
                if (prt.date.datepickerType === 2) {
                    ewr = true;
                    sdp = true;
                } else if (prt.date.datepickerType === 1) {
                    sdp = true;
                } else if (prt.date.datepickerType === 3) {
                    rng = true;
                    ranges = {
                        'Today': [moment(), moment()],
                        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                        'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
                    };
                } else if (prt.date.datepickerType === 4) {
                    input = $(self).find("input");
                    input.attr("data-date-minviewmode", "months");
                    input.attr("data-date-viewmode", "years");
                    input.attr("data-date-format", "mm/yyyy");
                    input.datepicker({
                        onRender: function (date) {
                        }
                    }).on('changeDate', function (ev) {
                        //  othis.SelectedDate = ev.date;
                        // othis.onChanged(ev);
                    }).data('datepicker');
                    return;
                }
                var option = {
                    timePicker: isdatetime,
                    showDropdowns: true,
                    showWeekNumbers: ewr,
                    autoUpdateInput: false,
                    autoApply: false,
                    enableWeek: ewr,
                    singleDatePicker: sdp,
                    locale: {
                        cancelLabel: 'Clear'
                    }
                    //locale: {
                    //    format: prt.dateformat.toUpperCase()
                    //}
                };
                if (isdatetime === true) {
                    option.locale = {
                        "format": prt.date.datetimeformat,
                        "separator": " - ",
                        "applyLabel": "Apply",
                        "cancelLabel": "Cancel",
                        "fromLabel": "From",
                        "toLabel": "To",
                        "customRangeLabel": "Custom",
                        "weekLabel": "W",
                        "daysOfWeek": [
                            "Su",
                            "Mo",
                            "Tu",
                            "We",
                            "Th",
                            "Fr",
                            "Sa"
                        ],
                        "monthNames": [
                            "January",
                            "February",
                            "March",
                            "April",
                            "May",
                            "June",
                            "July",
                            "August",
                            "September",
                            "October",
                            "November",
                            "December"
                        ],
                        "firstDay": 1
                    };
                } else {
                    if (prt.inputType === 7) {
                        option.locale = {
                            format: prt.date.dateformat.toUpperCase()
                        };
                    } else {
                        option.locale = {
                            format: prt.date.datetimeformat.toUpperCase()
                        };
                    }
                    
                }
                if ($.isEmptyObject(ranges) === false) {
                    option.ranges = ranges;
                }
                var picker = $(self).find("input").daterangepicker(option);

            } else if (prt.inputType === 8) {
                $(self).append(tmp);
                
            } else if (prt.inputType === 12) {
                $(self).append(tmp);
                $(self).find("input").data('parent', self);
                $(self).find("input").on("change", methods._fileSelection);
            }else if (prt.inputType === 13) {
                $(self).append(tmp);
                //data-slider-value
                $(self).find("input").bootstrapSlider({
                    id: "slider" + prt.id ,
                    min: prt.min,
                    max: prt.max,
                    value: prt.value,
                    handle:'square',
                    range: prt.range,
                    precision: prt.precision,
                    orientation: prt.orientation,
                    formatter: function (value) {
                        return 'Current value: ' + value;
                    }
                });
                $("#slider" + prt.id).addClass("ns-Slider");
            } else if (prt.inputType === 15) {
                $(self).append(tmp);
                $(self).colorpicker({
                    format: prt.color.format,
                    color: prt.color.defaultcolor,
                  colorSelectors: prt.color.colorSelectors
                }).on('changeColor', function (e) {
                    prt.color.onSelected(e.color);
                    });
            } else if (prt.inputType === 17) {
                $(self).append(tmp);
                var ty = '';
                if (prt.autofill.type === 'select') {
                    ty = 'select';
                } else {
                    ty = prt.autofill.getSource;
                }
                var op = {
                    dataSource: ty,
                    sortable: false,
                    placeholder: prt.autofill.placeholder,
                    tokensAllowCustom: prt.autofill.allowCustomText,
                    searchFromStart: prt.autofill.searchFromStart,
                    tokensMaxItems: 0
                };
                $('#aufill_' + prt.id).tokenize2(op);
                
            } else if (prt.inputType === 19) {
                $(self).append(tmp);
                $('#select_' + prt.id).selectpicker({
                    dropupAuto: prt.selectPicker.dropupAuto,
                    width: prt.width,
                    noneSelectedText: prt.noneSelectedText,
                    noneResultsText: prt.noneResultsText
                });
            } else {
                
                $(self).append(tmp);
                //if (prt.inputType === 10) {
                //    if (prt.text == 'true' || prt.text == true || prt.text ==1) {
                //        $(self).find('input').prop("checked", true);
                //    }                    
                //}
            }
        },
        _validatefile: function (fname,ext) {
            if (fname.length > 0) {
                var blnValid = false;
                for (var j = 0; j < ext.length; j++) {
                    var sCurExtension = ext[j];
                    if (fname.substr(fname.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }
                if (!blnValid) {
                    alert("Sorry, " + fname + " is invalid, allowed extensions are: " + ext.join(", "));
                    return false;
                }
            }
            return true;
        },
        _fileSelection: function (e) {
            var files = e.target.files;
            var self =$(this).data('parent');
            var file_list = self.find("ul");
            var file = '';  
            var prt= self.data('c_textbox');
            self.uploaders = [];
            var opt = {};
            $.extend(opt, prt.upload);
            for (var i = 0; i < files.length; i++) {
                file = files[i];
                if (prt.upload.maxSize > file.size.getInBytes()) {
                    if (methods._validatefile(file.name, prt.upload.fileExtension)) {
                        var cup = new ChunkedUploader(file, opt);
                        self.uploaders.push(cup);
                        if (prt.upload.showPreview === true) {
                            file_list.append('<li><a>' + file.name + '(' + file.size.formatBytes() + ')</a></li>');
                            file_list.show();
                        }
                    } else {
                        //if (prt.upload.showPreview == true) {
                        //    file_list.hide();
                        //}
                    }
                } else {
                    $(files).val("");
                    alert("The file you are trying to upload exceeds the " + othis.maxSize.getInBytes() + " MB attachment limit");
                    throw "Maximum file size to upload is " + othis.maxSize.getInBytes();
                }
            }

            if (self.uploaders.length > 0) {
                $.each(self.uploaders, function (i, uploader) {
                    uploader.start();
                });
            }
            else {
                return false;
            } 
        },
        _getRegExp: function (prt, self) {
            if (prt.inputType === 2) {                 
                return /^\d+$/;
            } else if (prt.inputType == 3) {
                   return /^-?\d*[.,]?\d{0,2}$/; // 2 decimal place
            } else if (prt.inputType === 4) {
                 return /^([a-zA-Z0-9 _-]+)$/;
            } else if (prt.inputType === 5) {
                if (prt.date.timeformat === 12) {
                       return /((1[0-2]|0?[1-9]):([0-5][0-9]) ?([AaPp][Mm]))/;
                } else {
                     return /^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/; //24 hrs
                }
            } else if (prt.inputType === 14) {
                 return /^(?:(?:https?|ftp):\/\/)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/\S*)?$/igm;
            }
            return prt.regexp;
        },
        _setRegExp: function (prt, self) {
            $(self).find("input[class=form-control]").inputFilter(function (value) {
                if (value === "")
                    return true;
                var rg = methods._getRegExp(prt, self);
                if (rg === "")
                {
                    return true;
                }
                var regex = new RegExp(rg);
                var rs = regex.test(value);
                methods._regValidation(self, rs);
                return rs;
            });             
        },
        _getTemplate:function (prt) {
            var tmp = "";
            var txt = "text";
            if (prt.textType === 1) { //textbox
                txt = "text";
            } else if(prt.textType ===2){ //password
                txt = "password";
            }
            if (prt.inputType === 10) { // checkbox
                txt = "checkbox";
            } else if (prt.inputType === 11) { // radio
                txt = "radio";
            }
            var hlp = "";
            if (prt.inputType === 1 || prt.inputType === 2 || prt.inputType === 3 || prt.inputType === 4 || prt.inputType === 5 || prt.inputType === 6 || prt.inputType === 13 ) {
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                var db = "";
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<input  " + db + " type='" + txt + "' id='"+ prt.id +"' class='form-control' placeholder=''/>";
                
                if (prt.note !== "") {
                    hlp = prt.note;                    
                }
                if (prt.limit === true) {
					hlp = hlp + " " + "(Length min-" + prt.min + " max-" + prt.max + ")";
                }                    
                tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + hlp + '</small>';
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
                
            } else if (prt.inputType === 7 || prt.inputType === 9) { // datepicker,datetimepicker
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<input  " + db + " type='" + txt + "' id='" + prt.id +"' class='form-control' placeholder=''/>";
                tmp = tmp + '<i class="glyphicon glyphicon-calendar fa fa-calendar"></i>';
                 
                if (prt.note != "") {
                    hlp = prt.note;
                }
                //if (prt.limit === true) {
                hlp = hlp + " " + "Date Format(" + prt.date.dateformat.toUpperCase() + ")";
                tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + hlp + '</small>';
                //}
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            } else if (prt.inputType === 10) {

                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<input  " + db + " type='checkbox'  id='" + prt.id +"'  class='form-control option-input checkbox' placeholder=''/>";
                if (prt.note != "") {
                    tmp = tmp + '<br/><small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            } else if (prt.inputType === 11) {
                var nm = "";
                if (prt.name !== "") {
                    nm = "name='" + prt.name + "'";
                }
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<input  " + db + " type='radio' " + nm + " id='" + prt.id +"'  class='form-control option-input radio' placeholder=''/>";                
                if (prt.note !== "") {
                    tmp = tmp + '<br/><small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            } else if (prt.inputType == 12) {
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                var mlt = "";
                if (prt.upload.selection == 'multiple') {
                    mlt = "multiple";
                }
                tmp = tmp  + '<label class="file-upload btn btn-default">';
                tmp = tmp + "<input  " + db + " type='file' " + mlt + " id='" + prt.id +"' class='form-control' placeholder=''/>";
                tmp = tmp + "Browse for file..";
                tmp = tmp + '</label>';
                if (prt.note != "") {
                    hlp = prt.note;
                }
                hlp = hlp + "(Upload format:" + prt.upload.fileExtension.join(",") + ")";
               
                tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + hlp + '</small>';
                if (prt.upload.showPreview == true) {
                    tmp = tmp + "<ul id='file_list' class='list' style='display: none;'> </ul>";
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }

            } else if(prt.inputType === 15) {
                tmp = ' <div data-toggle="tooltip " title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<div class='input-group colorpicker-component'>";
                tmp = tmp + "<input  " + db + " type='text' id='" + prt.id +"' class='form-control' placeholder=''/>";
                tmp = tmp + '<span class="input-group-addon"><i></i></span>';
                tmp = tmp + "</div>";
                if (prt.note != "") {
                    tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            }
            else if (prt.inputType === 16) {
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind !== "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                tmp = tmp + "<label class='switch' style='margin-bottom:0px !important'><input id='" + prt.id +"'  " + db + " type='checkbox' class='form-control' /> <span class='slider round'></span></label>";
                if (prt.note !== "") {
                    tmp = tmp + '</br><small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            } else if (prt.inputType === 17) {//selection
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind != "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                var sel = "";
               // if (prt.selectPicker.selection != 'single') {
                    sel = 'multiple';
               // }
                //tmp = tmp + "<input  " + db + " type='" + txt + "' class='form-control' placeholder=''/>";
                var ds = prt.autofill.datasource;
                tmp = tmp + "<select " + db + "   multiple   class='form-control' id='aufill_" + prt.id + "'>";
                $.each(ds, function (ids, so) {
                    var vf = "";
                    if (prt.autofill.valueField !== "") {
                        vf = so[prt.selectPicker.valueField];
                    } else {
                        vf = so[prt.autofill.displayField];
                    }
                    var ds = so[prt.autofill.displayField];
                    tmp = tmp + "<option value='" + ds + "' key='"+ vf +"'>" + ds + "</option>";
                });
                tmp = tmp + "</select>";
                if (prt.note !== "") {
                    tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            } else if (prt.inputType === 19) {
                tmp = ' <div data-toggle="tooltip" title="this is required field">';
                if (prt.enabletooltip === false) {
                    tmp = "";
                }
                var db = "";
                if (prt.databind != "") {
                    db = "data-bind='" + prt.databind + "'";
                }
                var sel = "";
                if (prt.selectPicker.selection !== 'single') {
                    sel = 'multiple';
                }                
                var ds = prt.selectPicker.datasource;
                if (prt.selectPicker.groupField !== "") {
                    var ugroup = methods._renderUniqueValue(ds, prt.selectPicker.groupField);
                    ugroup = ugroup.Unique;
                    tmp = tmp + "<select " + db + " " + sel + " class='form-control' id='select_" + prt.id + "'>";
                    //if (prt.selectPicker.selection === 'single') {
                    //    tmp = tmp + "<option value='-1'>-----Select-----</option>";
                    //}
                    $.each(ugroup, function (iug, ug) {
                        var fv = ug[prt.selectPicker.groupField];
                        var gv = prt.selectPicker.groupField;
                        var o = {};
                        o[gv] = fv;
                        var gitems = $.arryfilter(ds, o);
                        if (gitems.length > 0) {
                            tmp = tmp + "<optgroup label='" + fv + "'>";
                            $.each(gitems, function (ids, so) {
                                var vf = "";
                                if (prt.selectPicker.valueField !== "") {
                                    vf = so[prt.selectPicker.valueField];
                                } else {
                                    vf = so[prt.selectPicker.displayField];
                                }
                                var ds = so[prt.selectPicker.displayField];
                                tmp = tmp + "<option value='" + vf + "'>" + ds + "</option>";
                            });
                            tmp = tmp + "</optgroup>";
                        }
                    });                    
                    tmp = tmp + "</select>";
                } else {
                    tmp = tmp + "<select " + db + " " + sel + " class='form-control' id='select_" + prt.id + "'>";
                    if (prt.selectPicker.selection === 'single') {
                        tmp = tmp + "<option value='-1'>-----Select-----</option>";
                    }
                    $.each(ds, function (ids, so) {
                        var vf = "";
                        if (prt.selectPicker.valueField !== "") {
                            vf = so[prt.selectPicker.valueField];
                        } else {
                            vf = so[prt.selectPicker.displayField];
                        }
                        var ds = so[prt.selectPicker.displayField];
                        tmp = tmp + "<option value='" + vf + "'>" + ds + "</option>";
                    });
                    tmp = tmp + "</select>";
                }                
                 
                if (prt.note !== "") {
                    tmp = tmp + '<small id="emailHelp" class="form-text text-muted">' + prt.note + '</small>';
                }
                if (prt.enabletooltip === true) {
                    tmp = tmp + "</div>";
                }
            }
            if (prt.inputType === 5) {
                prt.min = 0;
                prt.max = 23;
                prt.stepmin = 15;
                prt.overflowtime = true;
            }
            return tmp;
        },
        _renderUniqueValue: function (ds,fld) {
            return $.getUniqueArray(ds, fld);
        },
        _regValidation: function (cthis,rs) {
           // var o = $(cthis).data("c_textbox");
            //console.log("input val:" + $(cthis).find("input").val());
            //console.log("input state:" + rs);
           // if (methods._setRegExp(o) != "") {
             //   var regex = new RegExp(methods._getRegExp(o));
            if (rs === false) {
                    $(cthis).find("[data-toggle=tooltip]").attr("title", "invalid input.please enter appropriate format");
                    $(cthis).find("[data-toggle=tooltip]").attr("data-original-title", "invalid input.please enter appropriate format");
                    $(cthis).find("[data-toggle=tooltip]").tooltip('show');
                    $(cthis).find("input").css("border", "1px solid red");
                    return false;
                } else {
                    $(cthis).find("input").css("border", "");
                   $(cthis).find("[data-toggle=tooltip]").tooltip('hide');
                }
            //}
        },        
        _showError: function (msg) {
            var cthis = this;
            $(cthis).find("[data-toggle=tooltip]").attr("title", msg);
            $(cthis).find("[data-toggle=tooltip]").attr("data-original-title", msg);
            $(cthis).find("[data-toggle=tooltip]").tooltip('show');
        },
        _hideError: function () {
            var cthis = this;
            $(cthis).find("[data-toggle=tooltip]").attr("title", "");
        },
        _isValid: function () {
            var cthis = this;
                    //1-text,2-number,3-decimal,4-alphaNumber,5-timeformat,6-email, //7-datepicker, 9-datetimepicker, 10-checkbox, 11-radio, 12-fileupload,
                //13-number range, 14.url,  15 color picker ,16 - yes/no,17 autextbox,19-combobox
            var o = $(cthis).data("c_textbox");            
            var v = methods.text.call(cthis);
            if (o.inputType === 1 ||
                o.inputType === 2 ||
                o.inputType === 3 ||
                o.inputType === 4 ||
                o.inputType === 6 ||
                o.inputType === 14) {                
                if (o.isRequired === true) {
                    if (v === "") {
                        return true;
                    }
                }                
                if (o.limit === true) {
                    var l = "";
                    if (o.inputType === 1 || o.inputType === 4 || o.inputType === 6 || o.inputType === 14) {
                        l = v.length;
                    } else if (o.inputType === 2 || o.inputType === 3) {
                        l = parseFloat(l);
                    }
                    if (l < o.min) {
                        methods._showError.call(cthis, "Entered value less than required");
                        return false;
                    } else {
                        methods._hideError.call(cthis);
                    }
                    if (l > o.max) {
                        methods._showError.call(cthis, "Entered  value greater than required");
                        return false;
                    } else {
                        methods._hideError.call(cthis);
                    }
                }
            }
            else if (o.inputType === 5) {
                // var va = $(this).find("input[class=form-control]").timepicki("getValue");                 
                    if (va === undefined) {
                        return false;
                    }                
                if (!methods._requiredValid.call(cthis, v)) {
                    return false;
                }
                if (o.min >= 0 && o.max > 0) {
                    if (o.min > v.h || o.max < v.h) {
                        methods._showError.call(cthis, "Entered value must between " + o.min + " to " + o.max + "");
                        return false;
                    } else {
                        methods._hideError.call(cthis);
                    }
                }
            }
            else if (o.inputType === 7) {
                if (o.isRequired === true) {
                    if (o.date.datepickerType === 1 || o.date.datepickerType === 4) {
                        if (va.startDate === "" || va.startDate === defaultDate) {
                            return false;
                        }
                    } else if (o.date.datepickerType === 2 || o.date.datepickerType === 3) {
                        if (va.startDate === "" || va.startDate === defaultDate || va.endDate === "" || va.endDate === defaultDate) {
                            return false;
                        }
                    }
                }
                if (o.limit === true) {
                    var minD = new Date(o.min);
                    var maxD = new Date(o.max);
                    var sd = new Date(va.startDate);
                    if (o.date.datepickerType === 1) {
                        if (minD > sd || maxD < sd) {
                            methods._showError.call(cthis, "Entered value must between " + minD.format(o.date.dateformat) + " to " + maxD.format(o.date.dateformat) + "");
                            return false;
                        }
                    } else if (o.date.datepickerType === 2 || o.date.datepickerType === 3) {
                        var ed = new Date(va.endDate);
                        //dateDiffInDays
                        if (o.date.datepickerType === 2) {
                            if (dateDiffInDays(sd, ed) !== 7) {
                                methods._showError.call(cthis, "Invalid Week selection.Please select from the calendar");
                                return false;
                            }
                        } else {
                            if (o.min !== "" && o.max !== "") {
                                if (minD > sd || maxD < sd || minD > ed || maxD < ed) {
                                    methods._showError.call(cthis, "Entered value must between " + minD.format(o.date.dateformat) + " to " + maxD.format(o.date.dateformat) + "");
                                    return false;
                                }
                            }
                        }
                    } else if (o.date.datepickerType === 4) {
                        if (sd.getFullYear() < o.min || sd.getFullYear() > o.max) {
                            methods._showError.call(cthis, "Entered value must between " + o.min + " to " + o.max + "");
                            return false;
                        }
                    }
                }

            }
            else if (o.inputType === 9) {
                var min = new Date(o.min);
                var max = new Date(o.max);
                if (o.isRequired === true) {
                    if (v.startDate === "" || v.startDate === defaultDate) {
                        return false;
                    }
                }
                if (o.limit === true) {
                    var sdt = new Date(va.startDate);
                    if (min > sdt || max < sdt) {
                        methods._showError.call(cthis, "Entered value must between " + min.format(o.date.datetimeformat) + " to " + max.format(o.date.datetimeformat) + "");
                        return false;
                    }
                }
            }
            else if (o.inputType === 12) {
                if (o.isRequired === true) {
                    if (v.files.length === 0) {
                        return false;
                    }
                }
                if (o.limit === true) {
                    if (v.files.length > o.max) {
                        methods._showError.call(cthis, "Maximum  " + o.max + " files to upload");
                    }
                }
            }
            else if (o.inputType === 13) {
                if (o.isRequired === true) {
                    if (v === "") {
                        return false;
                    }
                }
                if (o.limit === true) {
                    if (o.range === false) {
                        if (o.min > v || o.max < v) {
                            methods._showError.call(cthis, "Entered value must between  " + o.min + "  to " + o.max + "");
                        }
                    }
                }
            }
            else if (o.inputType === 15) {
                if (o.isRequired === true) {
                    if (o.isRequired === true) {
                        if (v === "") {
                            return false;
                        }
                    }
                }
            }
            else if (o.inputType === 17) {
                if (o.isRequired === true) {
                    if (o.isRequired === true) {
                        if (v.length === 0) {
                            return false;
                        }
                    }
                }
                if (o.limit === true) {
                    if (o.max < v.length) {
                        methods._showError.call(cthis, "Maximum you can enter only" + o.max);
                        return false;
                    }
                }
            }
            else if (o.inputType === 19) {
                if (o.isRequired === true) {
                    if (v === "") {
                        return false;
                    }
                }
                if (o.limit === true) {
                    if (o.max < v.split(",").length) {
                        methods._showError.call(cthis, "Maximum you can select only" + o.max);
                        return false;
                    }
                }
            }
            return true;                
        },
        _setCheckValue: function (v) {
            if (v === true || v === "true" || v===1) {
                return true;
            } else {
                return false;
            }
        },
        isValid: function () {
            return methods._isValid.call(this);
        },
        text: function (val) {
            var o = $(this).data("c_textbox");         
            if (val !== undefined) {
                o.text = val;
                //1-text,2-number,3-decimal,4-alphaNumber,5-timeformat,6-email, //7-datepicker, 9-datetimepicker, 10-checkbox, 11-radio, 12-fileupload,
                //13-number range, 14.url,  15 color picker ,16 - yes/no,17 autextbox,19-combobox    
                if (o.inputType === 1 ||
                    o.inputType === 2 ||
                    o.inputType === 3 ||
                    o.inputType === 4 ||
                    o.inputType === 6 ||
                    o.inputType === 14) {
                    $(this).find("input").val(val);
                }
                else if (o.inputType === 13) {
                    $(this).find("input").bootstrapSlider('setValue', val);
                }
                else if (o.inputType === 5) {
                    if (val !== "") {
                        var ti = 0;
                        var mi = 0;
                        var mer = "AM";
                        var d = {};
                        if (val instanceof Date) {
                            d = val;
                        } else if (!isNaN(Date.parse(val))) {
                            d = Date.parse(val);
                        } else {

                            return "";
                        }
                        ti = d.getHours();
                        mi = d.getMinutes();
                        mer = "AM";
                        if (o.date.timeformat === 12) {
                            if (ti === 0) { // midnight
                                ti = 12;
                            } else if (ti === 12) { // noon
                                mer = "PM";
                            } else if (ti > 12) {
                                ti -= 12;
                                mer = "PM";
                            }
                        }
                        $(this).find("input[class=form-control]").timepicki("setValue", [ti, mi, mer]);
                        //alert($(this).find("input[class=form-control]").timepicki("getValue").toString());
                    }
                }
                else if (o.inputType === 7 ||
                    o.inputType === 9) {
                    var sdate = val.startDate;
                    var edate = val.endDate;
                    if (o.inputType === 9) {
                        if (sdate !== "") {
                            $(this).find("input").data('daterangepicker').setStartDate(moment(sdate, o.date.datetimeformat));
                        }
                    } else {
                        //datepickerType: 1,  // 1- single selection date picker,2 weekpicker,3-range picker 4-yearmonth picker
                        if (o.date.datepickerType === 1) {
                            if (sdate !== "") {
                                $(this).find("input").data('daterangepicker').setStartDate(moment(sdate, o.date.dateformat));
                            }
                        } else if (o.date.datepickerType === 2 || o.date.datepickerType === 3) {
                            if (sdate !== "") {
                                $(this).find("input").data('daterangepicker').setStartDate(moment(sdate, o.date.dateformat));
                            }
                            if (edate !== "") {
                                $(this).find("input").data('daterangepicker').setEndDate(moment(edate, o.date.dateformat));
                            }
                        }
                        else if (o.date.datepickerType === 4) {
                            $(this).find("input").datepicker("setValue", new Date(moment(sdate)).getMonth() + "/" + new Date(moment(sdate)).getFullYear());
                        }
                    }
                }
                else if (o.inputType === 10 ||
                    o.inputType === 16) {
                    var v = methods._setCheckValue(val);
                    if (v === true) {
                        $(this).find("input[type=checkbox]").prop("checked", true);
                    } else {
                        $(this).find("input[type=checkbox]").prop("checked", false);
                    }
                    //return $(this).find("input[type=check]").is(':checked');
                }
                else if (o.inputType === 11) {
                    var r = methods._setCheckValue(val);
                    if (r === true) {
                        $(this).find("input[type=radio]").prop("checked", true);
                    } else {
                        $(this).find("input[type=radio]").prop("checked", false);
                    }
                }
                else if (o.inputType === 15) {
                    $(this).colorpicker("setValue", val);
                }
                else if (o.inputType === 16) {
                    $(this).colorpicker("setValue", val);
                }
                else if (o.inputType === 17) {
                    var sv = o.text.split(",");
                    ithis = this;
                    if (sv.length > 0) {
                        $.each(sv, function (i, v) {
                            $('#aufill_' + o.id).tokenize2().tokenAdd(v, v, true);
                        });
                    }

                }
                else if (o.inputType === 19) {
                    var os = {};
                    var sVal = "";
                    if (o.selectPicker.selection === 'multiple') {
                        sVal = o.text.split(',');
                    } else {
                        sVal = o.text;
                    }
                    if (sVal.length > 0) {
                        $('#select_' + o.id).selectpicker("val", sVal);
                    }
                }
            }
            else {
                //return $(this).find("input[type=check]").is(':checked');
                if (o.inputType === 1 ||
                    o.inputType === 2 ||
                    o.inputType === 3 ||
                    o.inputType === 4 ||
                    o.inputType === 6 ||
                    o.inputType === 14) {
                    return $(this).find("input").val();
                } else if (o.inputType === 5) {
                    return $(this).find("input[class=form-control]").timepicki("getValue");
                }
                else if (o.inputType === 7 || o.inputType === 9) {
                    var $inp = $(this).find("input");
                    var k = {};
                    var dd = $inp.data('daterangepicker');
                    if (o.inputType === 7) {
                        if (o.date.datepickerType === 1 || o.date.datepickerType === 2 || o.date.datepickerType === 3) {
                            if ($inp.val() !== "") {
                                k.startDate = dd.startDate.format("YYYY-MM-DD") + "T00:00:00";
                                k.endDate = dd.endDate.format("YYYY-MM-DD") + "T00:00:00";
                            } else {
                                k.startDate = defaultDate;
                                k.endDate = defaultDate;
                            }

                        }
                        else if (o.date.datepickerType === 4) {
                            k.startDate = $inp.data('datepicker').viewDate.toString();
                            k.endDate = defaultDate;
                        }
                    }
                    else if (o.inputType === 9) {
                        if ($inp.val() !== "") {
                            k.startDate = dd.startDate.toJSON();
                            k.endDate = dd.endDate.toJSON();
                        }
                        else {
                            k.startDate = defaultDate;
                            k.endDate = defaultDate;
                        }
                    }
                    return k;
                }
                else if (o.inputType === 10 || o.inputType === 16) {
                    return $(this).find("input[type=checkbox]").is(':checked');
                }
                else if (o.inputType === 11) {
                    return $(this).find("input[type=radio]").is(':checked');
                }
                else if (o.inputType === 12) {
                    //  $(this).find("input[type=file]").data('parent');
                    var cup = $(this).find("input[type=file]").data('parent');
                    //    o.upload.files = [];
                    if (cup.uploaders !== undefined) {
                        $.each(cup.uploaders, function (iu, up) {
                            if (up.isError !== undefined) {
                                if (up.isError === false) {
                                    var v = {};
                                    v.FileName = up.file.name;
                                    v.uniqueID = up.upload_token;
                                    v.extension = up.file.name.substring(up.file.name.lastIndexOf('.') + 1);
                                    o.upload.files.push(v);
                                }
                            }
                        });
                    }
                    return o.upload.files;
                }
                else if (o.inputType === 13) {
                    return $(this).find("input").bootstrapSlider('getValue');
                } else if (o.inputType === 15) {
                    return $(this).colorpicker("getValue");
                } else if (o.inputType === 17) {
                    return $('#aufill_' + o.id).tokenize2().toArray();
                } else if (o.inputType === 19) {
                    return $('#select_' + o.id).selectpicker("val");
                }                
            }            
        }
    };
    $.fn.Input = function (method) {
        if (methods[method] && method.charAt(0) !== '_') {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            var o = methods.init.apply(this, arguments);
            return o;
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.pagination');
        }
    };
})($);
// set input validation
(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup blur select contextmenu drop", function () {
            try {
                if (inputFilter(this.value)) {
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (this.hasOwnProperty("oldValue")) {
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                }
            } catch (e) {
                return;
            }
            
        });
    };
}($));
//bread crumbs
(function ($) {
    $.breadcrumb = {
        defaults: {
            items: [],
            itemClick: function(){}
        },
        item: {
            name: "",
            url: "",
            isactive: false,
            data: {},
            subItem:[]
        }
    };
    var methods = {
        init: function (options) {
           var self = $(this);
            var _options = $.extend(true, $.breadcrumb.defaults, options || {});
           // $.each(_options.items, function (ibc, bc) {
            

            var mm = [];
            for (ibc = 0; ibc < _options.items.length; ibc++){
                var bc = new Object();
                bc=_options.items[ibc];
                bc = $.extend(true,{},$.breadcrumb.item, bc);
                if (ibc === (_options.items.length - 1)) {
                    bc.isactive = true;
                }
                mm[ibc] = bc;
            };  
            _options.items = mm;
            self.data('breadcrumb', $.extend({}, _options, { initialized: true, waiting: false }));

            methods._render.call(self);
            return this;
        },                        
        _render: function () {
            
            var self = this;
            var opt = this.data("breadcrumb");
            self.html("");
            var str = "";
            str = str + '<nav aria-label="breadcrumb">';                       
            str = str + '<ol class="breadcrumb">';  
            $.each(opt.items, function (ibc, bc) {
                var issub = false;
                var subattr = "";
                var ebDrop = "";
                if (bc.subItem.length > 0) {
                    issub = true;
                    subattr = 'class="dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"';
                    ebDrop = "dropdown";
                }
                if (ibc === opt.items.length - 1) {
                    if (!bc.isactive) {
                        str = str + ('<li class="breadcrumb-item ' + ebDrop +'" index="' + ibc + '"><a href="#" ' + subattr +'>' + bc.name + '</a>');
                    } else {
                        str = str + ('<li class="breadcrumb-item active" index="' + ibc +'" aria-current="page">' + bc.name + '');
                    }
                } else {
                    if (!bc.isactive) {                       
                        str = str + ('<li class="breadcrumb-item ' + ebDrop +'" index="' + ibc + '"><a href="#" ' + subattr +'  >' + bc.name + '<i style="top:3px" class="glyphicon glyphicon-menu-right"></i></a>');
                    } else {
                        str = str + ('<li class="breadcrumb-item active ' + ebDrop +'" index="' + ibc +'" aria-current="page">' + bc.name + '<i style="top:3px" class="glyphicon glyphicon-menu-right"></i>');
                    }
                }
                str = str + '<div class="dropdown-menu">';
                $.each(bc.subItem, function (isb, sb) {
                    str = str + ' <a class="dropdown-item" href="#">' + sb.name + '</a>';
                });
                str = str + '</div>';               
                str =str + "</li>";
            });  
             
            str = str + '</ol>';
            str = str + '</nav>';
            $(self).append(str);

            $(self).find("li[class=breadcrumb-item]").click(function () {
                var idn = $(this).attr("index");
                var opt = $(self).data("breadcrumb");                
                opt.itemClick(idn);
            });
        },
        add: function (bItem) {
            var opt = $(this).data("breadcrumb");
            var bc = $.extend(true, {}, $.breadcrumb.item, bItem);
            opt.items.push(bc);
        },  
        addActive: function (bItem) {
            var opt = $(this).data("breadcrumb");
            $.each(opt.items, function (ibc, bc) {              
                    bc.isactive = false;                
            });

            bItem.isactive = true;
            var bc = $.extend(true, {}, $.breadcrumb.item, bItem);
            opt.items.push(bc);
            $(this).data("breadcrumb",opt);
            methods._render.call($(this));
        },
        remove: function (indx) {
            var opt = $(this).data("breadcrumb");
            var nit =[];
            $.each(opt.items, function (ibc, bc) {
                if (ibc <= indx) {
                    nit.push(bc);
                }
            });
            opt.items = nit;            
            methods._render.call($(this));
        }
    };
    $.fn.breadcrumb = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };

})($);
//input checkbox
(function ($) {
    $.inputCheckbox = {
        defaults: {
            checked: true,
            value: 0,
            name:"rb",
            checkType:10, ////10,11
            onChecked: function () {
            },
            onChanged: function () {

            },
            input: $.input.defaults
        }
    };
    var inputCheckbox = function ($e, options) {
        var _userOptions = (typeof options === 'function') ? { callback: options } : options;
       var _options = $.extend({}, $.inputCheckbox.defaults, _userOptions, options || {});
            _init = function () {
            var strtemp = '<div  class="row" style="border-radius:5px">';
            strtemp = strtemp + '<div class="col-md-1" style="width: 8%;margin-bottom: 5px;border: 1px solid #ccc;border-radius:5px 0px 0px 5px;background-color:#ddd">';
                strtemp = strtemp + '<span  plc="ck" id="basic-addon1"></span>';
                strtemp = strtemp + '</div>';
                strtemp = strtemp + '<div plc="txt" class="col-md-11"></div>';
                strtemp = strtemp + "</div>";
                $e.append(strtemp);
                _renderText();
            _renderCheck();
            
            $($e.find("span[plc='ck']").find('div')).css("margin", '6px 0px 6px 42px');
            $e.find("div[plc='txt']").css("padding", "0px");
            $e.find("div[plc='txt']").find("input").css({ 'border-radius':'0px 5px 5px 0px','border-left':'0px'});
            $e.find("span[plc='ck']").find('input[type="checkbox"]').css("top", "0px");
            },
            _renderText = function () {
                $e.find("div[plc='txt']").Input(_options.input);
            },
            _renderCheck = function () {
            $e.find("span[plc='ck']").Input({
                inputType: _options.checkType,
                text: _options.checked,
                name: _options.name
            });
            $e.find("span[plc='ck']").click(function () {
                $e.find("div[plc='txt']").Input('toggle');
            });
            

            },
            _debug = function (m) {
                if (_options.debug && typeof console === 'object' && (typeof m === 'object' || typeof console[m] === 'function')) {
                    if (typeof m === 'object') {
                        var args = [];
                        for (var sMethod in m) {
                            if (typeof console[sMethod] === 'function') {
                                args = (m[sMethod].length) ? m[sMethod] : [m[sMethod]];
                                console[sMethod].apply(console, args);
                            } else {
                                console.log.apply(console, args);
                            }
                        }
                    } else {
                        console[m].apply(console, Array.prototype.slice.call(arguments, 1));
                    }
                }
            };
        $e.data('inputcheck', $.extend({}, _options, { initialized: true, waiting: false }));
        _init();
    };
    $.fn.inputCheckbox = function (options) {
        return this.each(function () {
            var $this = $(this);
            data = $this.data("inputcheck");
            if (data && data.initialized) {
                return;
            }
            inputCheckbox($this, options);
        });
    };
})($);
// input checkbox list
(function ($) {
    $.inputCheckboxlist = {
        defaults: {
            enableSearch: true,
            enableAdd: false,
            enableRemove: false,
            title: "",
            datasource: [],
            checkType: 11,
            name:"rdb"
        }
    };
    var inputCheckboxlist = function ($e, options) {
        var _userOptions = (typeof options === 'function') ? { callback: options } : options;
        var _options = $.extend(true, $.inputCheckboxlist.defaults, _userOptions, options || {});
            _init = function () {
            _render();
                $e.find('[ckwelist=""]').weblist({
                    datakey: "",
                    content: {
                        template: "<div plc='container'></div>"
                    },
                    header: {
                        template: "<h3>" + _options.title + "</h3>"
                    },
                    datasource: _options.datasource,
                    enableCallback: false,
                    callback: {
                        url: "/Security/GetData",
                        param: "",
                        contenttype: 'json',
                        enableCrossDomain: true,
                    },
                    itemBind: function (itm, rw) {
                        $(itm).find("[plc='container']").inputCheckbox({
                            checkType: _options.checkType,
                            name: _options.name,
                            input: {
                                inputType:1
                            }
                        });
                    },
                    pagging: {
                        enable: true,
                        pagesize: 10,
                        pageStyle:'light-theme',
                    }
            });         
              

            };
            _render = function () {
                $e.append("<div ckwelist=''></div>");
        };
        _init();
        $e.data('inputCheckboxlist', $.extend({}, _options, { initialized: true, waiting: false }));
        };
        $.fn.inputCheckboxlist = function (options) {
            return this.each(function () {
                var $this = $(this);
                data = $this.data("inputCheckboxlist");
                if (data && data.initialized) {
                    return;
                }
                inputCheckboxlist($this, options);
            });        
    };
})($);
// form control
(function ($) {
        $.form = {
            defaults: {
                fields: [],
                title: ""
            },
            global: {

            }
        };
        $.form.field = {
            defaults: {
                id: '',
                fieldtype: 1,
                regex: "",
                isrequired: false,
                min: 0,
                max: 100,
                limit: false,
                texttype: 1,
                timeformat: 12,
                dateformat: 'mm/dd/yyyy',
                precision: 2,
                note: "",
                stephours: 1,
                stepmin: 10,
                colorSelectors: {
                    'black': '#000000',
                    'white': '#ffffff',
                    'red': '#FF0000',
                    'default': '#777777',
                    'primary': '#337ab7',
                    'success': '#5cb85c',
                    'info': '#5bc0de',
                    'warning': '#f0ad4e',
                    'danger': '#d9534f'
                },
                chuckSize: 200,
                maxSize: 5000,
                noneSelectedText: "Nothing Selected",
                noneResultsText: "No result found",
                container: 'body'
            }
        };
        var form = function ($e, options) {
            var _data = $e.data('form');
            var _userOptions = (typeof options === 'function') ? { callback: options } : options;
            var _options = $.extend({}, $.form.defaults, _userOptions, options || {});
                _init = function () {
                    $.each(_options.fields, function (ifld, fld) {
                        addField(fld);
                    });

                },
                _textbox = function (fld, type) {
                    if (fld.fieldtype === 8 || fld.fieldtype == 18 || fld.fieldtype === 19 || fld.fieldtype == 20) {

                        // 1- single selection date picker,2 weekpicker,3-range picker 4-yearmonth picker
                        dpt = 1;
                        if (fld.fieldtype === 8) {
                            dpt = 1;
                        } else if (fld.fieldtype === 18) {
                            dpt = 2;
                        } else if (fld.fieldtype === 19) {
                            dpt = 3;
                        } else if (fld.fieldtype === 20) {
                            dpt = 4;
                        };

                        $("#" + fld.id).Input(
                            {
                                id: "fld_" + fld.id,
                                limit: fld.limit,
                                max: fld.max,
                                inputType: type,
                                regexp: fld.regex,
                                textType: 1,
                                isrequired: fld.isrequired,
                                precision: fld.precision,
                              //  timeformat: fld.timeformat,
                                stephours: fld.stephours,
                                stepmin: fld.stepmin,
                                note: fld.note,
                                //dateformat: fld.dateformat,
                                //datepickerType: dpt,
                                 date: {
                                     datepickerType: dpt,   
                                     dateformat: fld.dateformat,  
                                     timeformat: fld.timeformat  //12,24  
                                }
                            }
                        );
                    } else {
                        $("#" + fld.id).Input(
                            {
                                id: "fld_" + fld.id,
                                limit: fld.limit,
                                max: fld.max,
                                inputType: type,
                                regexp: fld.regex,
                                textType: 1,
                                isrequired: fld.isrequired,
                                precision: fld.precision,
                               // timeformat: fld.timeformat,
                                stephours: fld.stephours,
                                stepmin: fld.stepmin,
                                note: fld.note,
                                date: {
                                    datepickerType: 1,  
                                    dateformat: fld.dateformat,  
                                    timeformat: fld.timeformat  
                                }
                            }
                        );
                    }

                },
                 addField = function (fld) {
                    var _fld = $.extend(true, $.form.field.defaults, fld);
                    //1-text 2-password
                    //1-text,2-number,3-decimal,4-alphaNumber,5-timeformat,6-email, //7-datepicker, 9-datetimepicker, 10-checkbox, 11-radio, 12-fileupload,
                    //13-number range, 14.url,  15 color picker ,16 - yes/no,17 autextbox,19-combobox  
                    switch (parseInt(_fld.fieldtype)) {
                        case 1: // textbox
                            _textbox(_fld, 1);
                            break;
                        case 2:// number textbox
                            _textbox(_fld, 2);
                            break;
                        case 3://alpha number textbox
                            _textbox(_fld, 4);
                            break;
                        case 4: // decimal
                            _textbox(_fld, 3);
                            break;
                        case 5: // time 
                            _textbox(_fld, 5);
                            break;
                        case 6:
                            _textbox(_fld, 6);
                            break;
                        case 7: // date picker
                            _textbox(_fld, 7);
                            break;
                        case 8:
                            _textbox(_fld, 9);
                            break;
                        case 9:
                            _textbox(_fld, 10);
                            break;
                        case 10:
                            _textbox(_fld, 11);
                            break;
                        case 11:
                            _textbox(_fld, 12);
                            break;
                        case 12:
                            _textbox(_fld, 13);
                            break;
                        case 13:
                            _textbox(_fld, 14);
                            break;
                        case 14:
                            _textbox(_fld, 15);
                            break;
                        case 15:
                            _textbox(_fld, 16);
                            break;
                        case 16:
                            _textbox(_fld, 17);
                            break;
                        case 17:
                            _textbox(_fld, 19);
                            break;
                        case 18:
                            _textbox(_fld, 9);
                            break;
                        case 19:
                            _textbox(_fld, 9);
                            break;
                        case 20:
                            _textbox(_fld, 9);
                            break;
                        case 21:
                            break;
                    }
                },
                _refresh = function () {

                },
                _debug = function (m) {
                    if (_options.debug && typeof console === 'object' && (typeof m === 'object' || typeof console[m] === 'function')) {
                        if (typeof m === 'object') {
                            var args = [];
                            for (var sMethod in m) {
                                if (typeof console[sMethod] === 'function') {
                                    args = (m[sMethod].length) ? m[sMethod] : [m[sMethod]];
                                    console[sMethod].apply(console, args);
                                } else {
                                    console.log.apply(console, args);
                                }
                            }
                        } else {
                            console[m].apply(console, Array.prototype.slice.call(arguments, 1));
                        }
                    }
                };
            $e.data('form', $.extend({}, _options, { initialized: true, waiting: false }));
            _init();
        };
        $.fn.form = function (options) {
            return this.each(function () {
                var $this = $(this);
                data = $this.data("form");
                if (data && data.initialized) {
                    return;
                }
                form($this, options);
            });
        };
})($);
//weblist
(function ($) {
    $.weblist = {
        defaults: {
            callback: {
                method: "POST",
                url: "",
                param: "",
                contenttype: 'json',
                enableCrossDomain: true
            },            
            datasource: [],
            enableCallback: true,
            datakey: "",
            header: {
                template: ""
            },
            enabletoolbar: true,
            toolbar: {  // refresh,export,edit,custom toolbar
                template: "",
                search: {
                    enable: true,
                    searchFields: [],
                    type: 'sw', //sw,ew,c,
                },
                sorting: {
                    enable: true,
                    sorting: [] // {field:"",type:asc or desc,
                },
                enableClumnSelect: false,
                position: 'top', //top bottom
                align: 'right' // left right
            },
            footer: {
                template: ""
            },
            pagging: {
                enable: true,
                pagesize: 10,
                position: "top",  //top,bottom
                align: 'left', //left,right,center,
                pageStyle:'light-theme',
            },
            enableSelection: true,
            selectionType: 'signle', // singe and multiple
            itemClick: function () {

            },
            content: {
                template: ""
            },
            itemBind: function () {

            },
            itemTemplate: ""

        }
    };
    var methods = {
         // _data = $e.data('weblist'),
         //      _userOptions = (typeof options === 'function') ? { callback: options } : options,
          //    _options = $.extend(true, $.weblist.defaults, _userOptions, _data || {}),              
        init : function (options) {
            self = this;
            var _options = $.extend(true, $.weblist.defaults, options || {});
            _options.currentPage = 0;
            $(this).data("weblist", _options);
                if (_options.enableCallback === true) {
                   methods._getdatasource.call(this);
                } else {
                    methods.refresh.call(this);
                    methods._refreshpagging.call(this);
            }
         
              return this;
            },
        refresh : function () {
                methods._container.call(this);
                methods._renderheader.call(this);
                methods._renderbody.call(this);
                methods._renderfooter.call(this);
                methods._rendertoolbar.call(this);
                methods._renderpaging.call(this);
        },
        _refreshpagging: function () {
           
        },
        _getdatasource : function () {
            var _options = $(this).data("weblist");
            var self = this;
                $.ajax({
                    method: _options.callback.method,
                    url: _options.callback.url,
                    data: _options.callback.param,
                    dataType: _options.callback.contenttype,
                    contentType: "application/json; charset=utf-8",
                    enableCrossDomain: _options.callback.enableCrossDomain,
                    success: function (s, v) {
                        _options.datasource = JSON.parse(s);
                        methods.refresh.call(self);
                    },
                    error: function () {

                    },
                    beforeSend: function () {

                    }
                });
            },
        _renderheader : function () {
            var _options = $(this).data("weblist");
                $(this).find("[wl='header']").append(_options.header.template);
            },
        _renderfooter : function () {
            var _options = $(this).data("weblist");
        },
        _getCurrentPosition : function () {
            var _options = $(this).data("weblist");
                return _options.currentPage * _options.pagging.pagesize;
            },
        _renderbody : function () {;
            var _options = $(this).data("weblist");
            $b = $(this).find("[wl='body']");
            var pval = methods._getCurrentPosition.call(this);
                //$.each(_options.datasource, function (ids, ds) {
            for (i = pval; i < pval + _options.pagging.pagesize; i++) {
                    var ds = _options.datasource[i];
                    var itm = $b.append("<div class='row' witem='" + i + "'> <div class='col-md-12'>" + _options.content.template + "</div></div>");
                };
                //});
                var delementrow = $b.find("[witem]");
                $.each(delementrow, function (ide, de) {
                    var ditems = $(de).find("[data-field]");
                    var indx = pval + ide;
                    if (indx < _options.datasource.length) {
                        var ds = _options.datasource[indx];
                        $.each(ditems, function (idi, ditem) {
                            var dfield = $(ditem).attr("data-field");
                            var dfieldtype = $(ditem).attr("field-type");
                            if (dfieldtype !== undefined) {
                                $(ditem).Input({
                                    inputType: dfieldtype
                                });
                            } else {
                                if (ds[dfield] !== undefined) {
                                    $(ditem).append(ds[dfield]);
                                }
                            }
                        });
                        _options.itemBind(de, ds);
                    }                    
                }
                );
            },
        _rendertoolbar : function () { },
        _renderpaging: function () {
            if ($(this)[0].id === "") {
                $(this)[0].id = uuidv4();
            }
            var _options = $(this).data("weblist");
           
            var self = $.extend(true, this, {});
            //$(this).find("[wl='page']")[0].parent = self;
              var o=  $(this).find("[wl='page']").pagination({
                    items: _options.datasource.length,
                    itemsOnPage: _options.pagging.pagesize,
                    displayedPages: 5,
                    cssStyle: _options.pagging.pageStyle,
                    currentPage: (parseFloat(_options.currentPage) + 1),
                    edges: 0,
                    firstText: "<<",
                    lastText: ">>",
                    onPageClick: function (pageNumber, ev) {                                               
                        var _options =  $("#"+this.parent).data("weblist");
                       _options.currentPage = pageNumber - 1;
                        methods.refresh.call($("#" + this.parent));
                    },
                    selectOnClick: true,
                    prevText: "Prev",
                    nextText: "Next", 
                     parent: $(this)[0].id
            });
         
            },
        _container : function () {
            var _options = $(this).data("weblist")
                $(this).html("");
                var h = "<div wl='header'></div>";
                var b = "<div wl='body'></div>";
                var pg = "<div wl='page'></div>";
                if (_options.pagging.enable !== true) {
                    pg = "";
                }
                var f = "<div wl='footer'>" + pg + "</div>";
                var tb = "<div></div>";
                if (_options.header.template === "") {
                    h = "";
                    isheader = false;
                }
                //if (_options.footer.template === "") {
                //    f = "";
                //    isfooter = false;
                //}
                if (_options.enabletoolbar === false) {
                    tb = "";
                }
                var cnt = "<div>" + h + tb + b + f + "</div>";
                
                $(this).append(cnt);
            },            
        _debug : function (m) {
                if (_options.debug && typeof console === 'object' && (typeof m === 'object' || typeof console[m] === 'function')) {
                    if (typeof m === 'object') {
                        var args = [];
                        for (var sMethod in m) {
                            if (typeof console[sMethod] === 'function') {
                                args = (m[sMethod].length) ? m[sMethod] : [m[sMethod]];
                                console[sMethod].apply(console, args);
                            } else {
                                console.log.apply(console, args);
                            }
                        }
                    } else {
                        console[m].apply(console, Array.prototype.slice.call(arguments, 1));
                    }
                }
            }
            //$e.data('weblist', $.extend({}, _options, { initialized: true, waiting: false }));        
    };
    $.fn.weblist = function (method) {
        var arg = arguments;
        return this.each(function () {
           // var $this = $(this);
            //data = $this.data("weblist");
            if (methods[method] && method.charAt(0) != '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }       
        });
    };
})($);
// labelfields
(function ($) {
    $.labelfields = {
        defaults: {
            items: [{
                label: "",
                field: {},
                help: "",
                format: "",
                id: ""
            }]
        },
        template: '<div class="control" id="{0}"></div>'
    };
    var methods = {
        init: function (options) {
			self = $(this);
			var _options = {};
			_options.items = [];
			$.each(options.items, function (io, o) {
				var settings = $.extend({}, $.labelfields.defaults.items[0], o);
				_options.items.push(settings);
				_options.template = $.labelfields.defaults.template;
			});

			//var _options = $.extend(true, options || {}, $.labelfields.defaults);
            $.each(_options.items, function (jk, ks) {
                ks.field.enabletooltip = false;               
				$("#" + ks.id).Input(ks.field);
				$("#" + ks.id).Input("text", ks.field.text);
                $($("#" + ks.id).find("input")[0]).parent().addClass("control");
                $("#" + ks.id).find("input").attr("placeholder", ks.label);
                $("#" + ks.id).find("input").removeClass("form-control");
                $("#" + ks.id).find("input").prop('required', true);
                //$("#" + ks.id).find('small').remove();
                $($("#" + ks.id).find("input")[0]).after("<label for=" + ks.field.id + ">" + ks.label + "</label>");
            });            
            self.data('lblfield', $.extend({}, _options, { initialized: true, waiting: false }));
        }
    };
    $.fn.labelfields = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };
})($);
// submit button
(function ($) {
    var fn = {
    };
    fn.options = {
        value: "",
        hover: function () { },
        onComplete: function (s, v) { },
        onError: function (s, v) { },
        onProgress: function () { },
        onValidate: function () { return true },
        animation: false,
        css: "",
        disable: false,
        enableLoading: false,
        backgroundColor: "",
        textColor: "",
        callback: {
            param: "",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            enableCrossDomain: true,
            dataType: 'json',
            url: ''
        }
    };
    fn._setOption = function (key, value) {
        //$.Widget.prototype._setOption.apply(this, arguments);                  
        if (key === 'disable') {
            if (value === true) {
                this._disable();
            } else {
                this._enable();
            }
        } else if (key === "value") {
            this.options.value = value;
            this.element.html(value);
        } else if (key === 'css') {
            this.options.css = value;
        }
        this._super(key, value);
        this.refresh();
    };
    fn.value = function (value) {
        if (value === undefined) {
            return this.options.value;
        }
        this.options.value = value;
        this.refresh();
    };
    fn.callback = function (callback) {
        if (callback === undefined) {
            return this.options.callback;
        } else {
            this.options.callback = callback;
        }
    };
    fn._setOptions = function (options) {
        this._super(options);
        this.refresh();
    };
    fn.oncallback = function () {
        var self = this;
        var $opn = self.options.callback;
        $.ajax({
            url: $opn.url,
            type: $opn.method,
            data: $opn.param ? $opn.param : "",
            dataType: $opn.dataType,
            cache: false,
            crossDomain: $opn.enableCrossDomain,
            traditional: true,
            contentType: $opn.contentType,
            success: function (s, v) {
                self.options.onComplete(s, v);
                self.refresh();
                self._enable();
            },
            error: function () {
                self.options.onError();
                self.refresh();
                self._enable();
            },
            beforeSend: function () {
                self.options.onProgress();
            }
        });
    };
    fn.refresh = function () {
        var self = this;
        this.element.html("");
        this.element.append("<button class='btn btn-default " + this.options.css + "' value=''><span>" + this.options.value + "</span></button>");
        this.element.find("button").on("click", function () {
            self._disable();
            $(this).html("<i class='fa fa-refresh fa-spin'></i>Loading...");
            if (self.options.onValidate()) {
                self.oncallback();
            }
        });
    };
    fn._create = function () {
        this.refresh();
    };
    fn._enable = function () {
        // if (this.$disable) {
        this.element.removeAttr("disabled");
        //}
    };
    fn._disable = function () {
        // if (this.$disable == null) {
        this.element.attr("disabled", "disabled");
        //}
    };
    $.widget("ui.Submit", fn);
})($);
// modalwindow
(function ($) {
    $.modalwindow = {
        defaults: {
            id: "",
            title: "",
            savetext: "Save",
            canceltext: "Cancel",
            buttonSave: true,
            buttonCancel: true,
            onClose: function () { },
            onShow: function () {

            },
            onSave: function () { }
        }
    };
    var methods = {
        init: function (options) {
			var self = $(this);
			var _options = $.extend({}, $.modalwindow.defaults, options);
             //var _options = $.extend(true, $.modalwindow.defaults, options || {});
            self.data('model', $.extend({}, _options, { initialized: true, waiting: false }));
            self.html("");
            self.append(methods._gettemplate.call($(this)));
            $('#' + _options.id).on('hide.bs.modal', function (e) {
                _options.onClose();
            });
            $('#' + _options.id).on('show.bs.modal', function (e) {
                _options.onShow();
            });
            $("#" + _options.id).find(".modal-footer").find("[action=save]").click(function () {
                _options.onSave();
			});
			$("#" + _options.id).find(".modal-footer").find("[action=cancel]").click(function () {
				$("#" + _options.id).modal('hide');
			});
             return this;            
        },
        _gettemplate: function () {
            var _options = this.data('model');
            var k = '<div class="modal fade" id="' + _options.id + '"  tabindex=" - 1" role="dialog" aria-hidden="true">';
			k = k + '<div class="modal-dialog modal-lg" role="document">';
            k = k + ' <div class="modal-content">';
            k = k + '<div class="modal-header">';
            k = k + '<h5 class="modal-title" id="exampleModalLabel">' + _options.title + '</h5>';
            k = k + '<button type="button" class="close" data-dismiss="modal" aria-label="Close">';
            k = k + '<span aria-hidden="true">&times;</span>';
            k = k + '</button>';
            k = k + '</div>';
            k = k + '  <div class="modal-body">';
            k = k + ' </div>';
            k = k + ' <div class="modal-footer">';
            if (_options.buttonCancel === true) {
                k = k + '<button type="button" class="btn btn-secondary" action="cancel">' + _options.canceltext + '</button>';
            }            
            if (_options.buttonSave === true) {
                k = k + '<button type="button" class="btn btn-primary" action="save">' + _options.savetext + '</button>';
            }
            k = k + '</div>';
            k = k + '</div>';
            k = k + '</div>';
            k = k+ '</div>';
            return k;
        },
        open: function () {          
            var self = $(this);
             var _options = self.data('model');
            $("#" + _options.id).modal({
                keyboard: true,
                backdrop: true,
                show:true
			});        
			//$("#" + _options.id).modal('show'); 
        },
        close: function () {
            var self = $(this);
            var _options = self.data('model');
            $("#" + _options.id).modal('hide');
        }

    };
    $.fn.modalwindow = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                  return methods.init.apply(this, arg);
            }
        });
    };
})($);
// dialog
(function ($) {
    $.dialogBox = {
        defaults: {
            type: 1,  // 1-alert 2-confirmation 3- remarks
            title: "",
            message: "",
            alertType:3, // 1-error 2-warning 3-information
            id:"",
            onAction: function () {

            },
            onCancel: function () {

            }
        }
    };
    var methods = {
        init: function (options) {
			var self = $(this);
			var _options = $.extend({}, $.dialogBox.defaults, options);
            //var _options = $.extend(true, $.dialogBox.defaults, options || {});
            self.data('dialogBox', $.extend({}, _options, { initialized: true, waiting: false }));
            if (_options.type === 1) {
                if (_options.title === "") {
                    _options.title = "Alert";
                }
                self.modalwindow({
                    title: _options.title,
                    id: _options.id,
                    buttonSave: false,
                    buttonCancel: true,
                    canceltext: "OK",
                    onClose: _options.onCancel
                });
                self.find(".modal-body").append(methods._getalertTemplate.call($(this)));
            }
            else if (_options.type === 2) {
                if (_options.title === "") {
                    _options.title = "Confirmation";
                }
                self.modalwindow({
                    title: _options.title,
                    id: _options.id,
                    buttonSave: true,
                    buttonCancel: true,
                    canceltext: "No",
                    savetext:"Yes",
                    onClose: _options.onCancel,
                    onSave: _options.onAction
                });
                self.find(".modal-body").append(_options.message);
            }            
        },
        _getalertTemplate: function () {
            var self = this;
            var _options = self.data('dialogBox');
            var icon = "";
            var cn = "";
            var clr = "";
            if (_options.alertType === 1) {
                icon = "fa-exclamation-triangle";
                cn = "Error";
                clr = "#a94442";
            } else if (_options.alertType === 2) {
                icon = "fa-info";
                cn = "Information";
                clr = "#31708f";
            } else if (_options.alertType === 3) {
                icon = "fa-check";
                cn = "Success";
                clr = "#3c763d";
            }
            var k = '<div class="row">';
            k = k + '<div class="col-lg-12">';
            k = k + '<div class="" role="alert">';
            k = k + '<div class="row vertical-align">';
            k = k + '<div class="col-md-1 text-center" style="color:'+ clr + '">';   // -3 #3c763d,  2-#31708f  1-#a94442                       
            k = k + ' <i class="fa ' + icon +' "></i>';  //fa fa-check fa-2x fa fa-info fa-2x
            k = k + '</div>';
            k = k + '<div class="col-md-11">';
            k = k + '<strong style="color:'+clr+'">' + cn +':</strong> ' + _options.message;
            k = k + '</div>';
            k = k + '</div>';
            k = k + '</div>';
            k = k + '</div>';
            k = k + '</div>'; 
            return k;
        },
        show: function () {
            var self = $(this);
            var _options = self.data('dialogBox');
            $("#" + _options.id).modal({
                keyboard: true,
                backdrop: true,
                show: true
            });            
        },
        hide: function () {
            var self = $(this);
            var _options = self.data('dialogBox');
            $("#" + _options.id).modal('hide');
        }
    };
    $.fn.dialogBox = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };
})($);
// wizard
(function ($) {
    $.wizard = {
        defaults: {
            steps: [],
            theme: "default",
            selected: 0,
            autoHeight: true,
            hidestep: [],
            id: '',
            useURLhash:false
        },
        step: {
            id: '',
            title: "",
            subtitle:""
        },
        template: "",
        navtemplate: ' <li><a href="#{0}">{1}<br /><small>{2}</small></a></li>',
        steptemplate: "<div id='{0}' class=''></div>"
    };
    var methods = {
        init: function (options) {
            var self = $(this);
            var _options = $.extend(true, $.wizard.defaults, options || {});
            for (a in _options.steps) {
                _options.steps[a] = $.extend(true, {}, $.wizard.step, _options.steps[a]);
            };
            self.data('wizard', $.extend({}, _options, { initialized: true, waiting: false }));
            self.append(methods._render.call(self));           
            var hidestep= methods._gethiddenStep.call(self);
            self.smartWizard({
                theme: _options.theme,
                selected: _options.selected,
                keyNavigation: true,
                autoAdjustHeight: _options.autoHeight,
                hiddenSteps: hidestep,
                useURLhash: _options.useURLhash,
                showStepURLhash: false,
                transitionEffect: 'fade'
            });
            self.on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {
                //alert("You are on step "+stepNumber+" now");
                if (stepPosition === 'first') {
                    $("#prev-btn").addClass('disabled');
                } else if (stepPosition === 'final') {
                    $("#next-btn").addClass('disabled');
                } else {
                    $("#prev-btn").removeClass('disabled');
                    $("#next-btn").removeClass('disabled');
                }
            });
        },
        _gethiddenStep: function () {
            var self = this;
            var _options = self.data('wizard');
            var ank = self.find("li > a");
            var hd = [];
            $.each(ank, function (is, ss) {
                if (_options.hidestep.length > 0) {
                    var a = $.arryfilter(_options.hidestep, { id: ss[0].id });
                    if (a.length > 0) {
                        hd.push(ss);
                    }
                }
                
            });
            return hd;
        },
        _render: function () {
            var self = this;
            var _options = self.data('wizard');
            var k = '<ul>';
            var content = "<div>";
            $.each(_options.steps, function (is, ss) {
                var a = [];
                a.push(ss.id);
                a.push(ss.title);
                a.push(ss.subtitle);
                k = k + StringFormat($.wizard.navtemplate, a);
                content = content + StringFormat($.wizard.steptemplate, a);                 
            });
            k = k + "</ul>";
            content = content +"</div>";
            k = k + content;
            return k;
        }
    };
    $.fn.wizard = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };

})($);
//name card
(function ($) {
    $.namecard = {
        defaults: {
            title: "",
            header: "",
            align: "left",
            bodyTemplate: "",
            image:"",
            imagePosition: "",  // top,bottom
            footerTemplate: "",
            theme: ' bg-light',  //border-light
            width:'18rem'
        }
    };
    var methods = {
        init: function (options) {
            var self = $(this);
            var _options = $.extend(true, $.namecard.defaults, options || {});
            self.data('namecard', $.extend({}, _options, { initialized: true, waiting: false }));
            methods._render.call(this);
        },
        _render: function () {  //text-center ,text-right
            var self = $(this);
            var _options = self.data('namecard');
            var align = '';
            if (_options.align === 'right') {
                align = 'text-right';
            } else if (_options.align ==='center') {
                align = 'text-center';
            }
            var t = '<div class="card ' + align + ' ' + _options.theme + '"  style="width:' + _options.width + '">';
            if (_options.header !== "") {
                t = t + "<div class='card-header'>" + _options.header + "</div >";
            }
            if (_options.image !== "" && _options.imagePosition ==='top') {
                t = t + '<img class="card-img-top" src="' + _options.image + '" alt=""/>';
            }
            t = t + '<div class="card-body">';
            t = t + '<h5 class="card-title">' + _options.title + '</h5>';
            t = t + '<div class="card-text">' + _options.bodyTemplate + '</div>';
            t = t + '</div>';
            if (_options.image !== "" && _options.imagePosition === 'bottom') {
                t = t + '<img class="card-img-bottom" src="' + _options.image + '" alt="">';
            }
            t = t + '<div class="card-footer">' + _options.footerTemplate + '</div>';
            t = t + '</div>';
            self.html("");
            self.append(t);
        }
    };
    $.fn.namecard = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };

})($);
// navigation bar
(function ($) {
    $.toolbar = {
        defaults: {
            linkbuttons: [],
            formElement: [],
            name:''
        }
    };
    var methods = {
        init: function (options) {
            var self = $(this);
            var _options = $.extend(true, $.toolbar.defaults, options || {});
            self.data('toolbar', $.extend({}, _options, { initialized: true, waiting: false }));
            methods._render.call(this);
        },
        _render: function () {
            var self = $(this);
            var _options = $(this).data("toolbar");
            var key = uuidv4();
            var k = '<nav class="navbar navbar-expand-lg navbar-light" >'; //navbar - light bg - light
            k = k + '<a class="navbar-brand" href="#">' + _options.name +'</a>';
            k = k + '<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#' + key + '" aria-controls="' + key +'" aria-expanded="false" aria-label="Toggle navigation">';
            k = k + '<span class="navbar-toggler-icon"></span>';
            k = k + '</button>';
            k = k + '<div class="collapse navbar-collapse" id="' + key + '">';
           // k = k + ' <a class="navbar-brand" href="#">' + _options.name + '</a>';
            k = k + '<ul class="navbar-nav mr-auto mt-2 mt-lg-0">';
            $.each(_options.linkbuttons, function (ict, ct) {
				k = k + '<li class="nav-item" >' + ct + '</li>';
            });
            k = k + '</ul>';
            $.each(_options.formElement, function (ict, ct) {
				k = k + '<div class="form-inline my-2 my-lg-0" >' + ct + '</div>';
            });           
            k = k + "</div>";
            self.html("");
            self.append(k);
        }
    };
    $.fn.toolbar = function (method) {
        var arg = arguments;
        return this.each(function () {
            if (methods[method] && method.charAt(0) !== '_') {
                return methods[method].apply(this, Array.prototype.slice.call(arg, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arg);
            }
        });
    };

})($);

Number.prototype.formatBytes = function () {
    var units = ['B', 'KB', 'MB', 'GB', 'TB'];
    bytes = this;
    for (intx = 0; bytes >= 1024 && intx < 4; intx++) {
        bytes /= 1024;
    }
    return bytes.toFixed(2) + units[intx];
};
Number.prototype.getInBytes = function () {
    bytes = this,
        bytes /= 1024;
    return bytes.toFixed(2);
};
function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
$.arryfilter = function (arr, criteria) {
    return arr.filter(function (obj) {
        if (obj !== undefined) {
            return Object.keys(criteria).every(function (c) {
                return obj[c] === criteria[c];
            });
        }
    });
};
const _MS_PER_DAY = 1000 * 60 * 60 * 24;
// a and b are javascript Date objects
function dateDiffInDays(a, b) {
    // Discard the time and time-zone information.
    const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());
    return Math.floor((utc2 - utc1) / _MS_PER_DAY);
}
StringFormat = function (src, strArray) {
    var str = src;
    var format = new RegExp("{[0-9]}", "g");
    return str.replace(format, function (item) {
        var intVal = parseInt(item.substring(1, item.length - 1));
        var replace;
        if (intVal >= 0) {
            replace = strArray[intVal];
        } else {
            replace = "";
        }
        return replace;
    });
};
$.getUniqueArray = function (arr, uniqueField) {
    var unique = {};
    var distinct = [];
    var all = [];
    for (var ij in arr) {
        if (typeof (unique[arr[ij][uniqueField]]) == "undefined") {
            //for (var km in groupFields) {
            //    arr[ij][groupFields[km] + "_arr"] = [];
            //}
            distinct.push(arr[ij]);
        }
        unique[arr[ij][uniqueField]] = 0;
        all.push(arr[ij]);
        //for (var km in groupFields) {
        //    var val = arr[ij][uniqueField];
        //    var _obj = new Object();
        //    _obj[uniqueField] = val;
        //    var ss = filter(distinct, _obj);
        //    ss[0][groupFields[km] + "_arr"].push(arr[ij][groupFields[km]]);
        //}
    }
    var uniArray = new Object();
    uniArray.Unique = distinct;
    uniArray.all = all;
    return uniArray;
};







