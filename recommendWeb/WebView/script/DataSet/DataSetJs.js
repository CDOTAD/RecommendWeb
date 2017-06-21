
$(document).ready(function () {

    /*$.getJSON('../../../api/User/GetLimit')*/

    $.getJSON('../../../api/User/GetLimit')
        .done(function (data) {

            if (data == null) {
                return;
            }
            else {
                allUserInfo = data;

                for (var i in allUserInfo) {
                    singleUserInfo = allUserInfo[i];

                    var row = "<tr> <td>"+singleUserInfo.UserId+"</td></tr>"
                    

                    $("#user-info").append(row);
                }

               
            }

        });


    $.getJSON('../../../api/Movie/GetLimit')
        .done(function (data) {

            if (data == null) {
                return;
            }
            else {
                allMovieInfo = data;

                for (var i in allMovieInfo) {
                    singleMovieInfo = allMovieInfo[i];

                    var row = "<tr>";

                    row += "<td>" + singleMovieInfo.MovieId + "</td>";

                    row += "<td>" + singleMovieInfo.Title + "</td>";

                    row += "<td>" + singleMovieInfo.Genres + "</td>";

                    row += "</tr>";

                    $("#movie-info").append(row);
                }


            }

        });


    $.getJSON('../../../api/Rating/GetLimit')
        .done(function (data) {

            if (data == null) {
                return;
            }
            else {
                allRatingInfo = data;

                for (var i in allRatingInfo) {
                    singleRatingInfo = allRatingInfo[i];

                    var row = "<tr>";

                    row += "<td>" + singleRatingInfo.UserId + "</td>";

                    row += "<td>" + singleRatingInfo.MovieId + "</td>";

                    row += "<td>" + singleRatingInfo.Rate + "</td>";

                    row += "<td>" + singleRatingInfo.TimeStamp + "</td>";

                    row += "</tr>";

                    $("#rating-info").append(row);
                }

            }
        });


    var genChart = echarts.init(document.getElementById('data-set-chart'));

    var userChart = echarts.init(document.getElementById('user-group-chart'));

    var movieChart = echarts.init(document.getElementById('movie-group-chart'));
    //用于统一设置echart的itemStyle
    var showConfig={
        normal: {
            label: {
                show:false
            },
            labelLine: {
                show:false
            }
        },
        emphasis: {
            label: {
                show:true
            },
            labelLine: {
                show:true
            }
        }
    };
    //用于统一设置echart的tooltip
    var tipConfig = {
        trigger: 'item',
        formatter:"{a} <br/> {b} : {c} ({d}%)"
    }
    //用于统一设置echart的toolbaox
    var toolBoxConfig = {
        show: true,
        feature: {
            restore:{show:true}
        }
    }

    genChart.setOption({
       
        series: [{
            name: '整体数据',
        }]
    });

    userChart.setOption({
        series: [{
            name: 'user',
            itemStyle: showConfig,
            data:[]
        }]
    })

    movieChart.setOption({
        series: [{
            name: 'movie',
            itemStyle: showConfig,
            data:[]
        }]
    })


    var totalUser;
    var totalMovie;
    var totalRating;

    $.getJSON('../../../api/User/GetTotal')
        .done(function (data) {

            totalUser = data;

        });

    $.getJSON('../../../api/Movie/GetTotal')
        .done(function (data) {

            totalMovie = data;

        });

    $.getJSON('../../../api/Rating/GetTotal')
        .done(function (data) {

            totalRating = data;
            
            genChart.hideLoading();

            genChart.setOption(
            {
                toolbox:toolBoxConfig,
                tooltip:tipConfig,
                calculable: true,
                series:[{
                    name: '整体数据',
                    type: 'pie',
                    radius: 200,
                    center: ['50%', '50%'],
                    itemStyle:showConfig,
                    data: [
                        { value: totalUser, name: 'user' },
                        { value: totalMovie, name: 'movie' },
                        { value: totalRating, name: 'rating' }
                    ]
                }]
            });
            
        });
    
    $.getJSON('../../../api/Rating/GroupLengthsByUser')
        .done(function (data) {
            userChart.hideLoading();

            var userGroup = [];
            var len = 20;
            while (len--) {
                userGroup.push({
                    name: data[len].UserId,
                    value:data[len].GroupLength
                })
            }

            userChart.setOption({
                toolbox:toolBoxConfig,
                tooltip:tipConfig,
                calculable: true,
                series: [{
                    name: 'user',
                    type: 'pie',
                    radius: 150,
                    itemStyle:showConfig,
                    center:['50%','50%'],
                    data: userGroup
                }]
            });

            
        });

    
    $.getJSON('../../../api/Rating/GroupLengthsByMovie')
        .done(function (data) {

            movieChart.hideLoading();

            var userGroup = [];
            var len = 20;
            while (len--) {
                userGroup.push({
                    name: data[len].MovieId,
                    value: data[len].GroupLength
                })
            }

            movieChart.setOption({
                toolbox:toolBoxConfig,
                tooltip: tipConfig,
                calculable:true,
                
                series: [{
                    name: 'movie',
                    type: 'pie',
                    radius: [20,150],
                    roseType:'radius',
                    itemStyle:showConfig,
                    center: ['50%', '50%'],
                    data: userGroup
                }]
            });

        });

});


