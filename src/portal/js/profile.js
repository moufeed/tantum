 



$(document).on('click','.btn-previous ,.btn-next',function(event){
    event.preventDefault();
    let $this = $(this);
    let num =  $this.data("page");
    let url = $this.data("url");
    let pagesize = $this.data("size") || 6;
    let pageNumber =  $this.hasClass("btn-next") ?  num + 1  :  num - 1 ;
    let totalPages = $this.data('total'); 
    let projectUrl = $this.data("project");
    let html = '';
    
     
    if(pageNumber <= totalPages && pageNumber >= 1){
        $('.profile-loader').css('display','flex');
        fetch(`${url}?PageSize=${pagesize}&pagenumber=${pageNumber}`)
        .then(res=>res.json())
        .then(response=> RenderingData(response));
   
   
       let RenderingData = ({data , pagination , success})=>{
           
           if(success){
               data.forEach(pro => {
                    html += `<div class="col-12 col-md-6 col-lg-4">
                    <figure class="gallery-item">
                    <img src="${pro.mainImage}" alt="@item.Title">
                    <figcaption>
                        <h2>${pro.title}</h2>
                        <a href ="${projectUrl}/${pro.slug}" class="open-project">View more</a>
                    </figcaption>
                    </figure>
                    </div>`;
               })
               $(".galleries-items").html(html);
                
               if(pagination.nextPage){
                  $('.btn-next').data('page', parseInt(pagination.pageNumber))
               }
               if(pagination.previousPage){
                   $('.btn-previous').data('page',parseInt(pagination.pageNumber))
               }
           }
           $('.profile-loader').css('display','none');
           
       }
    }
})

//--------------------------------------------------
var template = '<div class="mfp-figure">'+
                '<div>'+
                '<div class="mfp-counter"></div>'+
            '<div class="mfp-close"></div>'+
            '</div>'+
            '<div class="mfp-img"></div>'+
            '<div class="mfp-bottom-bar">'+
              '<div class="mfp-title"></div>'+
            '</div>'+
          '</div>';

$(document).on('click','.open-project',function(event){
    event.preventDefault();
    let $this = $(this);
    let url =$this.attr('href');

    fetch(url)
    .then(res => res.json())
    .then(response => open_magnific(response))
});

 


const open_magnific = (response)=>{
    let project = response.data;
   
    if(response.success){
        let items = [];
        items.push({
            src : project.mainImage,
            title : `
                    <div class="item-info">
                    <h3><span>${project.title != null ? project.title : ''}</span></h3>
                    <div class="summary">${project.summary != null ? project.summary : ''}</div></div>`
        });
        if(project.items.length > 0){
            project.items.forEach( item =>{
                items.push({
                    src : item.mainImage,
                    title : `
                    <div class="item-info">
                    <h3><span>${item.title != null ? item.title : ''}</span></h3>
                    <div class="summary">${item.summary != null ? item.summary : ''}</div></div>`
                });
            })
        }
        $.magnificPopup.open({
            items: items,
            type: 'image',
            mainClass: 'mfp-gallery',
            closeMarkup: '<button title="%title%" type="button" class="mfp-close btn btn-close">&#215;</button>',
            image: {
                markup: template
                  
             },
            gallery:{
                enabled:true,
                tCounter: '%curr% / %total%'
            }
          }, 0); 
    }
}
