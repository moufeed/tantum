

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
  
});      