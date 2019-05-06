

$(document).ready(function(){
    if($('.mission_slider').length){ 
        $('.mission_slider').owlCarousel({
            dots :true,
            loop :false,
            responsiveClass:true,
            rtl : $('.mission_slider').data("rtl") || false ,
            responsive:{
                0:{
                    items:1,
                },
                600:{
                    items:2,
                },
                1000:{
                    items:3,
                }
            }
        });
    }


    if($('.clients_slider').length){ 
        $('.clients_slider').owlCarousel({
            dots :false,
            loop :false,
            responsiveClass:true,
            rtl : $('.clients_slider').data("rtl") || false ,
            responsive:{
                0:{
                    items:1,
                    dots :true
                },
                600:{
                    items:3,
                    dots :true
                },
                1000:{
                    items:4,
                },
                1024:{
                    items:5,
                }
            }
        });
    }
  


    if($('.service-carousel').length){ 
        $('.service-carousel').owlCarousel({
            dots :true,
            loop :false,
            responsiveClass:true,
            rtl : $('.service-carousel').data("rtl") || false ,
            responsive:{
                0:{
                    items:1,
                },
                600:{
                    items:2,
                },
                1000:{
                    items:3,
                }
            }
        });
    }


});      