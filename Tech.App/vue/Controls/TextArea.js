
define(function () {
    var setTemplate = function (itype, ele, attr) {
        itype = parseInt(itype);

        if (itype === 1 || itype === 2 || itype === 3 || itype === 4 || itype === 5 || itype === 6 || itype === 13) {
            var d = document.createElement("div");
            $(ele).find('input').addClass("form-control");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            $(ele).find('input').appendTo(d);

            var hlp = "";
            if (attr.note !== undefined) {
                hlp = attr.note;
            }
            if (attr.limit === true) {
                hlp = hlp + " " + "(Length min-" + attr.min + " max-" + attr.max + ")";
            }
            $(d).append('<small id="emailHelp" class="form-text help-text-muted">' + hlp + '</small>');
            $(ele).append(d);
        }
        else if (itype == 7 || itype == 9) { // datepicker
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            $(ele).find('input').appendTo(d);
            //$(d).append(' <i class="fa fa-calendar"></i>');
            $(ele).append(d);
        } else if (itype == 8) {
            var d = document.createElement("div");
            $(ele).find('textarea').addClass("form-control");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            $(ele).find('textarea').appendTo(d);

            var hlp = "";
            if (attr.note !== undefined) {
                hlp = attr.note;
            }
            if (attr.limit === true) {
                hlp = hlp + " " + "(Length min-" + attr.min + " max-" + attr.max + ")";
            }
            $(d).append('<small id="emailHelp" class="form-text help-text-muted">' + hlp + '</small>');
            $(ele).append(d);
        }
        else if (itype == 10 || itype == 11) {

            //var d = document.createElement("div");
            // $(d).attr("data-toggle", "tooltip");
            // $(d).attr("title", "this is required field");
            //$(ele).find('input').appendTo(d);
            $(ele).find('input').addClass("form-control option-input checkbox");
            //  if (prt.limit === true) {
            // var  hlp = hlp + " " + "(Length min-" + 0 + " max-" + 100 + ")";
            //  }
            ///$(d).append('<small id="emailHelp" class="form-text help-text-muted">' + hlp + '</small>');

        } else if (itype == 12) {
            $(ele).find('input').addClass("form-control");
            $(ele).find('input').attr("multiple", "");
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            var l = document.createElement("label");
            $(l).addClass("file-upload btn btn-primary");
            $(l).html("Browse for file..");
            $(d).append(l);
            $(ele).find('input').appendTo(l);
            $(ele).append(d);
        }
        else if (itype === 15) {
            var d = document.createElement("div");
            $(d).addClass("input-group colorpicker-component");
            $(ele).find('input').appendTo(d);
            $(d).append('<span class="input-group-addon"><i></i></span>');
            $(ele).append(d);
        } else if (itype == 16) {
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            var l = document.createElement("label");
            $(l).addClass("switch");
            $(l).css("margin-bottom", "0px");
            $(d).append(l);
            $(ele).find('input').appendTo(l);
            $(l).append("<span class='slider round'></span>");
            $(ele).append(l);

        }
    }
    return Vue.component('tztextarea',
        {
            template: '<div><textarea rows="5"  v-model="text" id="" @blur="handleInput"  @input="handleInput" class="form-control" placeholder=""/></div>'
            ,
            props: ['value', 'type', 'inputType', 'attribute', 'req', 'id'],
            data: function () {
                return {
                    required: true,
                    length: 200,
                    with: 200,
                    height: '',
                    text: this.value,
                    attr:this.attribute
                }
            },
            created: function () {

            },
            watch: {

            },
            mounted: function () {
                var othis = this;
                this.$nextTick(function () {
                    this.attr.inputType = 8;
            
                    setTemplate(this.attr.inputType, this.$el, this.attr);
                    $(this.$el).find("textarea").Input(this.attr);
                    $(this.$el).find("textarea").Input("text", this.text);                
                    $(this.$el).find('textarea').keypress(function () {
                        othis.text = $(this).val();
                        othis.$emit("text", othis.text);
                    });
                    $(this.$el).find('textarea').blur(function () {
                        //othis.text = $(this).val();
                        //othis.$emit("text", othis.text);
                    });
                    $(this.$el).find('textarea').change(function () {

                        //othis.text = $(this).val();

                        othis.$emit("text", othis.text);
                    });
                    $(this.$el).find('textarea').css({ "border": "1px solid #aaa", "border-radius": "5px" });
                });
            },
            updated: function () {
                //    $(this.$parent.$el).find('input').css("border", "1px solid black");
                //    alert($(this.$parent.$el).html());

            },
            destroyed: function () {
                //   $(this.$parent.$el).find('input').css("border", "1px solid black");
                //    alert($(this.$parent.$el).html());
            },
            methods: {
                handleInput: function (e) {
                    //  alert(this.text);
                    console.log(this.text);
                    this.$emit('input', this.text);
                },
                setValue: function (value) {
                    this.height = value;
                },
                addStyle: function () {

                },
                isrequired: function () {
                    if (this.required == true) {
                        return 'required';
                    }

                }, getType: function () {
                    if (this.attr.inputType == "number") {
                        return "date";
                    }
                }
            },
            computed: {
                 
                getType: {
                    get: function () {
                        var txt = "text";
                        if (this.attr.inputType == 1) { //textbox
                            txt = "text";
                        } else if (this.attr.inputType == 2) { //password
                            txt = "text";
                        }
                        if (this.attr.inputType == 10 || this.attr.inputType == 16) { // checkbox
                            txt = "checkbox";
                        } else if (this.attr.inputType == 11) { // radio
                            txt = "radio";
                        } else if (this.attr.inputType == 12) {
                            txt = "file";
                        }
                        return txt;
                    },
                    set: function () {

                    }

                },
               
            }
            // router: router
        });
});