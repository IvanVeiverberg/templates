namespace Templates.Api.Utils
{
    public static class Defaults
    {
        public const string HtmlTemplate = @"
<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<style>
    {{ style }}
</style>
</head>
<body>
    {{ content }}
</body>
</html>";

        public const string DefaultStyle = @"
h1 {
    font-family: 'Arial';
    font-size: 16pt;
    font-weight: 700;
    text-decoration: underline;
    line-height: 1.15em;
}
h2 {
    font-family: 'Arial';
    font-size: 12pt;
    font-weight: 700;
    line-height: 1.15em;
}
h3 {
    font-size: 10pt;
    font-weight: 700;
    line-height: 1.15em;
}
p {
    font-size: 10pt;
    font-weight: 400;
    line-height: 1.5em;
}";
    }
}
