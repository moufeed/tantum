 

var revapi112;
jQuery(document).ready(function () {
 

 



    if (jQuery("#main_slider").revolution == undefined) {
        revslider_showDoubleJqueryError("#main_slider");
    } else {
        jQuery('#main_slider').show().revolution({
            sliderType: 'standard',
            sliderLayout: 'fullwidth',
            responsiveLevels: [1240, 1024, 778, 480],
            gridwidth:[1240, 1024, 778, 480],
            delay: 9000,
            gridheight:[600, 600, 600, 480],
            navigation: {
                arrows: {
                    enable: true,
                    style: 'zeus',
                    tmp: '<div class="tp-title-wrap"><div class="tp-arr-imgholder"></div></div>',
                    hide_onleave: false
                },
                touch: {
                    touchenabled: "on",
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: "horizontal",
                    drag_block_vertical: false
                },
                bullets: {
                    enable: false,
                }
            } 
        });
    }
})
