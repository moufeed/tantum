


function toggleBackToTopBtn() {
    let amountScrolled = 500;
    if ($(window).scrollTop() > amountScrolled) {
        $("a.back-to-top").fadeIn("slow");
    } else {
        $("a.back-to-top").fadeOut("slow");
    }
}

$(document).on("click",".back-to-top", function(e) {
    e.preventDefault();
    $("html,body").animate({
        scrollTop: 0 
    }, 700);
    return false;
})

$(document).ready(function(){

  $('.nav-item:nth-of-type(1) .nav-link ').addClass("active");

  $('.scroll-link').on('click',function(e){
    e.preventDefault();
    var $this = $(this); 
    $('.nav-link').removeClass("active");
    $this.addClass("active")
    var id = $this.attr('href');
    $('html, body').animate({
        scrollTop: $(id).offset().top - 75
    }, 1000);
})


  $("body").append("<a href='#' class='back-to-top'><i class='fa fa-angle-up'></i></a>");

  let sticky = $('#sticky-header');
  let  headerHeight = sticky.outerHeight();
  $(window).on('scroll', function () {
    toggleBackToTopBtn();


    let scroll = $(window).scrollTop();
    if (scroll < headerHeight) {
      sticky.removeClass('sticky');
      $('body').css("padding-top",0)
    } else {
      sticky.addClass('sticky');
      $('body').css("padding-top",headerHeight)
    }
  });
}) 