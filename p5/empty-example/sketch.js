new p5(); //to allow usage of random() without defining it in setup()

let canvasH = 800;
let canvasW = 600;

var setup = function(){
  createCanvas(canvasH, canvasW);
}


//Initializing blocks
var block = {x: 250, y: 250, w: random(10,80), h: random(10,80), xspeed: random(0,8),yspeed: random(0,8), hue: 0};

var i = 0;
var i2 = 0;
var draw = function(){
  noStroke();
  colorMode(HSB, 100);
  fill(block.hue, 100, 100);
  block.hue += random(0.1,1);;
  
  //Rotation
  applyMatrix();
  translate(block.x,block.y);
  rotate(i);
  rect(0-block.w/2,0-block.h/2,block.w,block.h);
  resetMatrix();
  
   applyMatrix();
  translate(block.y,block.x);
  rotate(-i);
  rect(0-block.w/2,0-block.h/2,block.w,block.h);
  resetMatrix();
  
  //Implementing Speed
  block.x += block.xspeed;
  block.y += block.yspeed;
  
  //Bouncing logic
  if (block.y > canvasH - 50){
    block.yspeed *= -1;
  }
  if(block.y < 0){
    block.yspeed *= -1;
  }
  if (block.x > canvasW){
    block.xspeed *= -1;
  }
  if(block.x < 0){
    block.xspeed *= -1;
  }
  if(block.hue > 100){
    block.hue = 0;
  }
  
  //Rotation speed
  i+=random(0.1,0.3);
  i2+=random(0.1,0.3);
};