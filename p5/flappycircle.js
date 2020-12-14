new p5();
//Variables//

var setup = function(){
  setInterval(createSmoke,100);
  createCanvas(850,500);
};

var coins = [];
var player = {x: 150, y: 250, size: 50};
var gravity = 0;
var goUp = false;
var crashed = false;
var score = 0;
var gap = {height:300, y: 250};
var walls = [];
var wallTimer = 0;
var bg = loadImage("bg.jpg");
var moving = 0;
var timer = 0;
var angle = 0;
var angleUI = 0;
var powerUp = 0;
var smoke = [];
var coinsCollected = 0;
var EXP = 0;
var rand1 = 0;
var rand2 = 0;
var rand3 = 0;
var timer = 0;
var HP = 100;
var coinUI = 0;
var hurt = false;
var coinRate = 5;

//Program//

var draw = function() {
  timer++;
  image(bg, moving,0,650,500);
  image(bg, moving+650,0,650,500);
  image(bg, moving+650+650,0,650,500);
  
drawPlayer();

doSmoke();
  
  if(!crashed){
    movePlayer();
    doCoin();
    drawWalls();
    if(moving > -650){
    moving--;
  } else{
    moving = 0;
  }
    
  } else {
    youLoseScreen();
  }
  
  drawUI();
  
  if(coinsCollected%10+1 === 0){
    coinRate++;
  }
  
};












//Functions//



var drawWalls = function(){
  for(var wall of walls){
    noStroke();
    fill(119, 48, 232);
    rect(wall.x,wall.y,wall.w,wall.h);
    wall.x -= 3;
    
    if (wall.x < player.x && wall.x + wall.w > player.x){
      if(player.y - player.size/2 < wall.y + wall.h &&
        player.y + player.size/2 > wall.y){
        HP--;
        hurt = true;
          if(HP < 0){
            crashed = true;
          }
      } else {
        hurt = false;
      }
      
    } 
  }
  
  if(wallTimer <= 0){
    wallTimer = 16;
    gap.y += 25 * floor(random(3) - 1);
    if(gap.y < 150){
      gap.y = 150;
    }
    if(gap.y > 350){
      gap.y = 350;
    }
    
  var newWall;
    newWall = {x:700, y:0, w:50, h: gap.y - gap.height /2};
    walls.push(newWall);
    
    newWall = {x:700, y:gap.y + gap.height/2, w:50, h: 300};
    walls.push(newWall);
  }
  wallTimer -=1;
};

var doCoin = function(){
  
  var filteredCoins = coins.filter((coin) => {return coin.x > 0 && !coin.colected});
  coins = filteredCoins;
  
  if(random(0,100) < coinRate){
    var newCoin = {x: 900, y:random(0,500), size: 20, collected: false};
    coins.push(newCoin);
  }
  for (var coin of coins){
    if(coin.collected){
      continue;
    }
  fill(255,255,0);
  ellipse(coin.x,coin.y,coin.size,coin.size);
  
  coin.x -= 3;
  
  var playerRadius = player.size/2;
  var coinRadius = coin.size/2;
  var touchDistance = playerRadius + coinRadius;
  
  if(dist(player.x,player.y,coin.x,coin.y) < touchDistance){
    coin.collected = true;
    if(powerUp < 9){
      if(coinsCollected%10 ===0){
        powerUp++;
      }
    }
    coinsCollected++;
    score += powerUp;
  }
  }
};

var mousePressed = function() {
  if(mouseButton ===LEFT){
    goUp = true;
    
    if(crashed){
      crashed = false;
      player.y = 250;
      gravity = 0;
      score = 0;
      coins = [];
      walls = [];
      powerUp = 0;
      coinsCollected = 0;
      EXP = 0;
      HP = 100;
      hurt = false;
      coinRate = 5;
    }
  }
};

var mouseReleased = function(){
  if(mouseButton ===LEFT){
    goUp = false;
  }
};

var movePlayer = function() {
  if(goUp){
    gravity -= 0.4;
  } else {
    gravity +=0.4;
  }
  
  if(gravity < -5){
    gravity = -5;
  }
  if(gravity > 6){
    gravity = 6;
  }
  player.y += gravity;
  
  if(player.y > 500 || player.y <0 ){
    HP -= 100;
    crashed = true;
  }
  
};

var drawPlayer = function() {
  if(hurt){
  fill(255,0,0);
  } else {
    fill(0,0,255);
  }
  ellipse(player.x,player.y,player.size, player.size);
  applyMatrix();
  translate(player.x,player.y);
  rotate(angle);
  angle += 0.1;
  fill(0,0,255);
  if(powerUp > 0){
    ellipse(50,0,10, 10);
  }
  if(powerUp > 1){
    ellipse(-50,0,10, 10);
  }
  if(powerUp > 2){
    ellipse(0,50,10, 10);
  }
  if(powerUp > 3){
    ellipse(0,-50,10, 10);
  }
  fill(112, 255, 150);
  if(powerUp > 4){
    ellipse(35,35,10, 10);
  }
  
  if(powerUp > 5){
    ellipse(-35,-35,10, 10);
  }
  if(powerUp > 6){
    ellipse(-35,35,10, 10);
  }
  if(powerUp > 7){
    ellipse(35,-35,10, 10);
  }
  if(powerUp > 8){
    powerUp = 8;
  }
  
  resetMatrix();
 
};

var createSmoke = function() {
  var newSmoke = {x:player.x,y:player.y, size:10};
  smoke.push(newSmoke);
};

var youLoseScreen = function(){
  fill(255);
  textSize(24);
  text("Game Over",200,200);
  text("Click to Restart");
};

var doSmoke = function(){
  smoke = smoke.filter((s) => {return s.x > -50});
  
  for (var s of smoke){
    fill(200,200,200,175);
    ellipse(s.x,s.y,s.size,s.size);
    s.x -= 4;
    s.size++;
  }
};

var drawUI = function(){
 
    if(powerUp < 9){
    EXP = coinsCollected % 10;
    }
    else {
      EXP = 10;
    }
    
  
  fill(180);
  rect(700,0, 150,800);
  fill(0,0,255);
  arc(775,230, 120,120, 0, EXP * (2*PI)/11);
  fill(255);
  ellipse(775, 230,110,110);
  fill(180);
  ellipse(775, 230,90,90);
  
  
  fill(255);
  textSize(24);
  text("Score: " + score,720,50);
  fill(0,0,255);
  text("Score", 740, 140);
  text("Multiplier", 725, 160);
  textSize(40);
  text(powerUp + "x",755, 245);
  
  textSize(20);
  fill(255,255,0);
  text("CoinRate: " + coinRate + "%", 710, 90);
  
  strokeWeight(5);
  
  
  
  
  
  
  
  applyMatrix();
  translate(775,230);
  rotate(angleUI);
  angleUI += 0.05;
  fill(0,0,255);
  if(powerUp > 0){
  ellipse(50, 0,10,10);
  }
  if(powerUp > 1){
  ellipse(-50, 0,10,10);
  }
  if(powerUp > 2){
  ellipse(0, 50,10,10);
  }
  if(powerUp > 3){
  ellipse(0, -50,10,10);
  }
  fill(112, 255, 150);
  if(powerUp > 4){
  ellipse(35, 35,10,10);
  }
  if(powerUp > 5){
  ellipse(-35, -35,10,10);
  }
  if(powerUp > 6){
  ellipse(-35, 35,10,10);
  }
  if(powerUp > 7){
  ellipse(35, -35,10,10);
  }
 

  
  resetMatrix();
  
  if(timer%30===0){
    rand1 = random(0,255);
    rand2 = random(0,255);
    rand3 = random(0,255);
  }
  
  

  
  //HP red bar
  fill(255,0,0);
  rect(100,10, 100 * 2, 30);
  
  //HP green bar
  fill(0,255,0);
  rect(100,10, HP * 2, 30);
  
  fill(255,255,0);
  rect(90,55, coinsCollected, 20);
  
  
  fill(0,0,255, 180);
  ellipse(30,30, 150,150);
  fill(184, 193, 209, 150);
  ellipse(30,30, 140,140);
  
  translate(50,30);
fill(255,0,0);

textSize(20);
  text("HP",0,0);
  
  
  
fill(255,255,0);
  text("Coins",-20, 40);
  
  
  
  
};