
if(site_lang == 'ar'){
    $.extend( $.validator.messages, {
        required: "هذا الحقل إلزامي",
        remote: "يرجى تصحيح هذا الحقل للمتابعة",
        email: "رجاء إدخال عنوان بريد إلكتروني صحيح",
        url: "رجاء إدخال عنوان موقع إلكتروني صحيح",
        date: "رجاء إدخال تاريخ صحيح",
        dateISO: "رجاء إدخال تاريخ صحيح (ISO)",
        number: "رجاء إدخال عدد بطريقة صحيحة",
        digits: "رجاء إدخال أرقام فقط",
        creditcard: "رجاء إدخال رقم بطاقة ائتمان صحيح",
        equalTo: "رجاء إدخال نفس القيمة",
        extension: "رجاء إدخال ملف بامتداد موافق عليه",
        maxlength: $.validator.format( "الحد الأقصى لعدد الحروف هو {0}" ),
        minlength: $.validator.format( "الحد الأدنى لعدد الحروف هو {0}" ),
        rangelength: $.validator.format( "عدد الحروف يجب أن يكون بين {0} و {1}" ),
        range: $.validator.format( "رجاء إدخال عدد قيمته بين {0} و {1}" ),
        max: $.validator.format( "رجاء إدخال عدد أقل من أو يساوي {0}" ),
        min: $.validator.format( "رجاء إدخال عدد أكبر من أو يساوي {0}" )
    });
}


var options = {
    highlight: function(element) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function(element) {
        $(element).removeClass('is-invalid');
    },
    errorElement: 'div',
    errorClass: 'invalid-feedback',
    errorPlacement: function(error, element) {
        if(element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
}


$('#DepartForm').validate({...options,submitHandler: (form)=> {
    var $form = $(form);
    var btn = $form.find('.btn-send');
    btn.attr('disabled' ,"disabled");
    $form.valid();
    var url = $form.attr('action');
    var message ={};
    data = $form.serializeArray();
    $.each(data,function(key,val){
        message[val.name] = val.value;
    })
    $.ajax({  
        type: 'POST',
        url: url,
        data: message,  
    })
    .done(function (data) {
        btn.removeAttr('disabled');
            if(data.success){
                $form[0].reset();
                $("#alert-success").removeClass("d-none").addClass("show");
            }
    })
    .fail(function (jqXHR, Status) {
        btn.removeAttr('disabled');
    });

    return false;
}
});


$('#ContactForm').validate(
    { ...options,submitHandler: (form)=> {
                 
            var $form = $(form);
            var btn = $form.find('.float-btn');
                btn.attr('disabled' ,"disabled");
                btn.find(".fa-paper-plane-o").addClass("d-none");
                btn.find(".fa-spinner").removeClass("d-none");
                $form.valid()
                var url = $form.attr('action');
                var message ={};
                data = $form.serializeArray();
                $.each(data,function(key,val){
                    message[val.name] = val.value;
                })
                
                $.ajax({  
                    type: 'POST',
                    url: url,
                    data: message,  
                })
                .done(function (data) {
                    btn.removeAttr('disabled');
                    btn.find(".fa-paper-plane-o").removeClass("d-none");
                    btn.find(".fa-spinner").addClass("d-none");
                        if(data.success){
                            $form[0].reset();
                            $("#alert-success").removeClass("d-none").addClass("show");
                        }
                })
                .fail(function (jqXHR, Status) {
                    btn.removeAttr('disabled');
                    btn.find(".fa-paper-plane-o").removeClass("d-none");
                    btn.find(".fa-spinner").addClass("d-none");
                });

                return false;
        },
    }



);
