﻿namespace Yacaba.Domain.Models {
    public class WeatherForecast {

        public DateTime Date { get; set; }
        public Int32 TemperatureC { get; set; }
        public String Summary { get; set; } = default!;
        public Int32 TemperatureF => 32 + (Int32)(TemperatureC / 0.5556);

    }
}
