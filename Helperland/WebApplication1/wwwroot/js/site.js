
    const nav=document.querySelector('.nav_bar')
    window.addEventListener('scroll', fixnav)
    function fixnav(){
            if(window.scrollY>nav.offsetHeight+50){
        nav.classList.add('active')
    }
    else{
        nav.classList.remove('active')
    }
        }
