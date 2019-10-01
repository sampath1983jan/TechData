
String.prototype.format = String.prototype.f = function () {
    var s = this,
        i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

define(function () {
    return Vue.component('toast',
        {
            template: '<div :style="setposition" :id="id"></div>',
            props: ["toas", "id","position"],
            data: function () {
                return {    
                    toasts: this.toas,                    
                    template: '<div    id="{0}" class="toast" data-autohide="false">'
                        + '<div class="toast-header">'
                        + '<img src="..." class="rounded mr-2" alt="...">'
                        + '<strong class="mr-auto"></strong>'
                        + '<small>11 mins ago</small>'
                        + '<button type="button" class="ml-2 mb-1 close">'
                        + ' <span aria-hidden="true">&times;</span>'
                        + '</button>'
                        + '</div>'
                        + ' <div class="toast-body">'                        
                        + ' </div></div>',
                  
                }
            },
            created: function () {

            },
            watch: {
                value: function (newVal, oldVal) { // watch it
                    this.text = newVal;
                    if (this.text == "") { this.text = 0; }
                    var inval = 0;
                    var othis = this;
                    var showPercent = window.setInterval(function () {
                        $(othis.$el).find(".circle").attr("stroke-dasharray", inval + ",100");
                        inval = inval + 1;
                        if (inval > newVal) {
                            clearInterval(showPercent);
                        }
                    }, 1); // Runs at a rate based on the animation's
                }
            },
            mounted: function () {
                var othis = this;               
                this.$nextTick(function () {
                    $.each(this.toasts, function (it, itval) {
                      //  debugger;
                        var el = $("#" + othis.id).append(othis.template.f("toz_" + it));
                      //  $(el).attr("id", "toz_"+it);
                        $("#toz_" + it).find(".toast-body").append(itval.body);
                        $("#toz_" + it).find(".toast-header").find("strong").append(itval.header);
                    });                       
                    $('.toast').toast("show");    
                });
            },
            updated: function () {       
                var othis = this;           
                $.each(this.toasts, function (it, itval) {                
                    othis.push(itval);
                });                
                $('.toast').toast("show");    
            },
            
            destroyed: function () {
          //      debugger;;
            },
            methods: {
                close: function (index) {
                    var othis = this;
                    $("toz" + index).click(function () {
                        othis.toasts.splice(index, 1);
                    });  
                },
                show: function () {
                    $('.toast').toast("show");    
                          
                },
                hide: function (idx) {
                    this.toasts.splice(idx, 1);
                }, 
                resetIndex: function () {
                    $.each(othis.toasts, function (idx, v) {
                        var id = "toz_" + idx;
                        $("#" + id).find(".toast-header").find("[type=button]").attr("index", this.toasts.length);
                    })


                },                  
                push: function (item) {                    
                    var othis = this;                 
                                       
                    var id = "toz_" + this.toasts.length;
                    var el = $("#" + othis.id).append(othis.template.f("toz_" + this.toasts.length));
                    $("#" + id).find(".toast-body").append(item.body);
                    $("#" + id).find(".toast-header").find("strong").append(item.header);
                    $("#" + id).find(".toast-header").find("[type=button]").attr("index", this.toasts.length);
                    $("#toz_" + this.toasts.length).toast("show");
                    //$(this).parent().parent().fadeIn(600, function () {
                    //    $("#toz_" + this.toasts.length).toast("show");
                    //});
                        $(this).parent().parent().animate({
                            //width: ["toggle", "swing"],
                           // height: ["toggle", "swing"],
                            //"left": "+=250px" 
                            opacity: "toggle"
                        }, 600, "linear", function () {
                           
                        });

                    this.toasts.push(item);
                
                    $("#" + id).find(".toast-header").find("[type=button]").click(function () {                       
                        var index = $(this).attr("index");                       
                        othis.toasts.splice(index, 1);
                        $(this).parent().parent().fadeOut(600, function () {
                            $(this).remove();
                        });

                        //$(this).parent().parent().animate({
                        //    //width: ["toggle", "swing"],
                        //   // height: ["toggle", "swing"],
                        //    //"left": "+=250px" 
                        //    opacity: "toggle"
                        //}, 600, "linear", function () {
                        //        $(this).remove();
                        //});
                    });  

                }
                ,
                handleInput: function (e) {

                },
            },
            computed: {
                getToast: function () {
                    var othis = this;
                    $.each(this.toasts, function (i, v) {
                        v.template = othis.item;
                    });
                    return this.toasts;
                }
                ,
                setposition: function () {
                    var o = {};
                    o.position = "fixed";
                    if (this.position == "top left" || this.position == "left top") {
                        o.left = "20px";
                        o.top = "60px";
                    } else if (this.position == "right top" || this.position == "top right") {
                        o.right = "20px";
                        o.top = "60px";
                    } else if (this.position == "bottom right" || this.position == "right bottom") {
                        o.bottom = "20px";
                        o.right = "20px";
                    } else if (this.position == "left bottom" || this.position == "bottom left") {
                        o.bottom = "20px";
                        o.left = "20px";
                    } else {
                        o.left = "80%";
                       
                    }
                    return o;
                }

            }


        });
});