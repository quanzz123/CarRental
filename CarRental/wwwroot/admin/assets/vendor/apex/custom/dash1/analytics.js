var options = {
		chart: { height: 300, type: "area", toolbar: { show: !1 } },
		dataLabels: { enabled: !1 },
		stroke: { curve: "smooth", width: 3 },
		series: [
			{ name: "Sales", data: [10, 40, 15, 40, 20, 35, 20, 10, 31, 43, 56, 29] },
			{ name: "Revenue", data: [2, 8, 25, 7, 20, 20, 51, 35, 42, 20, 33, 67] },
		],
		grid: {
			borderColor: "#dfd6ff",
			strokeDashArray: 5,
			xaxis: { lines: { show: !0 } },
			yaxis: { lines: { show: !1 } },
		},
		xaxis: {
			categories: [
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
			],
		},
		yaxis: { labels: { show: !1 } },
		colors: ["#7920d1", "#d5cdff"],
		markers: {
			size: 0,
			opacity: 0.3,
			colors: ["#684af6", "#aa99ff"],
			strokeColor: "#ffffff",
			strokeWidth: 2,
			hover: { size: 7 },
		},
	},
	chart = new ApexCharts(document.querySelector("#analytics"), options);
chart.render();
