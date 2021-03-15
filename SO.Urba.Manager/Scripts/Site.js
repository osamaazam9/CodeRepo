$(document).ready(function () {

  //  alert('got here 1');

    if (!Modernizr.input.placeholder) { // on non-supporting browsers

        // fixes Placeholders IE issue
        Placeholders.init();
       
    }
}
 );