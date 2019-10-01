define(function () {
    return Vue.component('tzSearch',
        {
            template: '<div><input  type="text"/></div>',
             props:["req"],
            data: function () {
                return {
                    request: this.req
                };
            },            
            mounted: function () {            
                var othis = this;            
                this.$nextTick(function () {             
                    $(this.$el).find("input").Search({
                        type: this.request.type,
                        data: this.request.data,
                        contentType: this.request.contentType,
                        dataType: this.request.dataType,
                        url: this.request.url,
                        crossDomain: this.request.crossDomain,
                        searchFields: this.request.searchFields,
                        onComplete: function (data) {
                            alert(JSON.stringify( data));
                        },
                        onError: function (jqXhr, textStatus, errorMessage) {
                            alert(errorMessage);
                        }
                    });
                });
            },             
            // router: router
        });
});
