<template>
      
</template>
<script>

    
    module.exports = {
        name: "AppDeisgner",   
        components: {
            //'comp-list': a,
            //'comp-form': CompForm,
            //'form-list': fmlist
        },
        data: function () {
            return {               
                AppID: this.appid,
                App: {},
                Complist: [],
            }
        },
        beforeRouteEnter(to, from, next) {
          //  next();
        },
        beforeRouteUpdate(to, from, next) {
           // next();
        },
        props: ["appid"],
        created: function () {
            debugger;
     
        },
        methods: {
         
            getApp: function () {
                var that = this;
                $.ajax('/MyApp/' + this.AppID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data                     
                        success: function (data, status, xhr) {   // success callback function
                            that.App = data;

                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            
        },        
    }
   
</script>
