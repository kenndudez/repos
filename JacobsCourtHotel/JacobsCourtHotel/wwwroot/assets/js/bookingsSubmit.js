function calculate() {
    var dd=document.getElementById('datedepart');
    var da=document.getElementById('ArrivalDate');
    var total=document.getElementById('total_cost');
    //var hotel=document.getElementById('hotel5');
    
    //no hotel room selected or not depart date set or not arrival date set
    //or departing before arrival (nonsense) - set total to ZERO and exit the function
    
    if ( !(dd.value*1) || !(da.value*1) || da.value>dd.value ) {
        total.value='0';//you can set it to 'not allowed' also if you wish (instead of '0')
        return;
    }
    
    var days = Math.round( ( 
            dd.value - 
            da.value 
        ) / 86400 ); //timestamp is in seconds
    var cost = days * prices;
    if (isNaN(cost))
        cost = 0; //or set to "invalid input" - but this line should not be needed at this point
    total.value = cost;
    }