<template>
    <div>
        User {{ id }} {{ this.$route.params.action}}
        <div>{{Client.ClientName}}</div>
        <div>{{Client.ClientNo}}</div>
        <div>{{Client.ClientHost}}</div>
    </div>
</template>
<!--<script type="text/x-template" id="anchored-heading-template">
    <div>
        User {{ id }} {{ this.$route.params.action}}
        <div>{{ClientName}}</div>
        <div>{{ClientNo}}</div>
        <div>{{Client.ClientHost}}</div>
    </div>
</script>-->

<script>
    module.export = {        
    props: ['id'],
    data: function () {
        return {
            Client: {}
        }
    },
    updated: function () {
        this.getClient(this.$route.params.id);
    },
    created: function() {
        this.getClient(this.$route.params.id);
    },

    methods: {
        getClient: function (id) {
            var that = this;
            $.ajax('/Client/Get',
                {
                    type: "GET",
                    dataType: 'jsonp', // type of response data
                    data: { clientid: id },
                    // timeout milliseconds
                    success: function (data, status, xhr) {   // success callback function
                        that.Client = data;
                        console.log(that.Client);
                    },
                    error: function (jqXhr, textStatus, errorMessage) { // error callback
                        alert(errorMessage);
                    }
                });
        }
    }
    }
        </script>      