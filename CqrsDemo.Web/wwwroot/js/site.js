$.fn.editable.defaults.mode = 'popup';

$(document).ready(function () {
    $('.cancel-order').editable({
        params: function (params) {
            var data = {};
            data['orderId'] = params.pk;
            data['reason'] = params.value;
            return data;
        },
    }); 
});

$(document).ready(function () {
    $('.changeavailability-dispo').editable({
        params: function (params) {
            var data = {};
            data['dispoDeviceId'] = params.pk;
            data['newAvailability'] = params.value;
            return data;
        },
    });
});