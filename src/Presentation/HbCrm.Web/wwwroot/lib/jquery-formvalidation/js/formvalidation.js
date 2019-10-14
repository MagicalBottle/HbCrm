; (function ($) {
    $.fn.formvalidation = function (options) {
        return $(this).validate({
            focusInvalid: true,
            onkeyup: function (element) { $(element).valid(); },
            submitHandler: function (form) {
                if (options.submitHandler) {
                    options.submitHandler(form);
                }
            },
            rules: options.rules,
            messages: options.messages,
            errorPlacement: function (error, $element) {
                $element.tooltip("dispose");//清除以前的气泡
                $element.tooltip({//重新生成气泡
                    title: error.text(),
                    placement: 'bottom'
                });
                $element.addClass("is-invalid");//添加未验证样式
                $element.removeClass("is-valid");//清除已验证样式
            },
            success: function (str, element) {
                $element = $(element);
                $element.tooltip("dispose");//清除以前的气泡
                $element.addClass("is-valid");//添加已验证样式
                $element.removeClass("is-invalid");//清除未验证样式
            }
        })
    }
})(jQuery);