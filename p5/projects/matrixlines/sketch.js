var i =0;
var t=0;
var setup = function(){
  createCanvas(800,500)
}

var draw = function(){
  background(0,0,0);
 
  
  
  
    
      stroke(0,255,0, 255);
      for(var i = -500; i < 3000; i+=50){
        //Vertical
        line(0+i+t,-1000+t,0+i+t,3000+t);
        //Horizontal
        line(-1000+t, 0+i+t, 3000+t, 0+i+t);
      }
      
      
      for(var j = 0; j < 3000; j+=50){
        //Vertical
        line(0+j-t,-1000-t,0+j-t,3000-t);
        //Horizontal
        line(-1000-t, 0+j-t, 3000-t, 0+j-t);
      }
      
      if(t < 250){
        t+=0.5;
      } else {
        t = 0;
      }
}