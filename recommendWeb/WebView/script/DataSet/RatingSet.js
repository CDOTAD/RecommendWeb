$(document).ready(function () {

    var topUserChart = echarts.init(document.getElementById('top-user-chart'));

    var topMovieChart = echarts.init(document.getElementById('top-movie-chart'));

    var showConfig = {
        normal: {
            label: {
                show: false
            },
            labelLine: {
                show: false
            }
        },
        emphasis: {
            label: {
                show: true
            },
            labelLine: {
                show: true
            }
        }
    };

    var tipConfig = {
        trigger: 'item',
        formatter: "{a} <br/> {b} : {c} ({d}%)"
    };

    var toolBoxConfig = {
        show: true,
        feature: {
            restore: { show: true }
        }
    };

    topUserChart.setOption({
        series: [{
            name:'活跃用户',
        }]
    })

    topMovieChart.setOption({
        series: [{
            name:'热门电影',
        }]
    })

    $.getJSON('../../../api/Rating/GroupLengthsTopUser')
        .done(function (data) {

            topUserChart.hideLoading();

            var userGroup = [];
            for (var i in data) {
                userGroup.push({
                    name: data[i].Id,
                    value:data[i].GroupLength
                })
            }

            topUserChart.setOption({
                toolbox: toolBoxConfig,
                tooltip: tipConfig,
                calculable: true,
                
                series: [{
                    name: '活跃用户',
                    type: 'pie',
                    radius: [20, 200],
                    roseType: 'radius',
                    itemStyle: showConfig,
                    center: ['50%', '50%'],
                    data:userGroup
                }]
            })
        });

    $.getJSON('../../../api/Rating/GroupLengthsTopMovie')
        .done(function (data) {

            topMovieChart.hideLoading();

            var movieGroup = [];
            for (var i in data) {
                movieGroup.push({
                    name: data[i].Id,
                    value:data[i].GroupLength
                })

            }

            topMovieChart.setOption({
                toolbox: toolBoxConfig,
                tooltip: tipConfig,
                calculable: true,
                
                series: [{
                    name: '热门电影',
                    type: 'pie',
                    radius: [20, 200],
                    roseType: 'radius',
                    itemStyle: showConfig,
                    center: ['50%', '50%'],
                    data:movieGroup
                }]
            })
        });
});