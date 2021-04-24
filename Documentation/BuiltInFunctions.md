


ActionFunctions

* [BodyParameter](#BodyParameter)
* [HttpMethod](#HttpMethod)
* [Parameters](#Parameters)
* [ReturnType](#ReturnType)
* [Url](#Url)

ParametersFunctions

* [ToTypeScript](#ToTypeScript)

StringFunctions

* [ToCamelCase](#ToCamelCase)
* [ToUpperFirst](#ToUpperFirst)
* [ToLowerFirst](#ToLowerFirst)
* [SplitIntoSeparateWords](#SplitIntoSeparateWords)

SymbolsFunctions

* [WhereNamespaceStartsWith](#WhereNamespaceStartsWith)
* [WhereNamespaceEndsWith](#WhereNamespaceEndsWith)
* [WhereNamespaceMatches](#WhereNamespaceMatches)
* [WhereNameStartsWith](#WhereNameStartsWith)
* [WhereNameEndsWith](#WhereNameEndsWith)
* [WhereNameMatches](#WhereNameMatches)
* [ThatHaveAttribute](#ThatHaveAttribute)
* [ThatArePublic](#ThatArePublic)
* [ThatAreStatic](#ThatAreStatic)

TypeFunctions

* [AllReferencedTypes](#AllReferencedTypes)
* [ToTypeScriptDefault](#ToTypeScriptDefault)
* [ToTypeScriptType](#ToTypeScriptType)
* [Unwrap](#Unwrap)

TypesFunctions

* [ThatImplement](#ThatImplement)
* [ThatInheritFrom](#ThatInheritFrom)


ArrayFunctions

* [Add](#Add)
* [AddRange](#AddRange)
* [Compact](#Compact)
* [Concat](#Concat)
* [Cycle](#Cycle)
* [Each](#Each)
* [Filter](#Filter)
* [First](#First)
* [InsertAt](#InsertAt)
* [Join](#Join)
* [Last](#Last)
* [Limit](#Limit)
* [Map](#Map)
* [Offset](#Offset)
* [RemoveAt](#RemoveAt)
* [Reverse](#Reverse)
* [Size](#Size)
* [Sort](#Sort)
* [Uniq](#Uniq)
* [Contains](#Contains)

HtmlFunctions

* [Strip](#Strip)
* [Escape](#Escape)
* [UrlEncode](#UrlEncode)
* [UrlEscape](#UrlEscape)

MathFunctions

* [Abs](#Abs)
* [Ceil](#Ceil)
* [DividedBy](#DividedBy)
* [Floor](#Floor)
* [Format](#Format)
* [IsNumber](#IsNumber)
* [Minus](#Minus)
* [Modulo](#Modulo)
* [Plus](#Plus)
* [Round](#Round)
* [Times](#Times)

RegexFunctions

* [Escape](#Escape)
* [Match](#Match)
* [Matches](#Matches)
* [Replace](#Replace)
* [Split](#Split)
* [Unescape](#Unescape)

StringFunctions

* [Escape](#Escape)
* [Append](#Append)
* [Capitalize](#Capitalize)
* [Capitalizewords](#Capitalizewords)
* [Contains](#Contains)
* [Empty](#Empty)
* [Whitespace](#Whitespace)
* [Downcase](#Downcase)
* [EndsWith](#EndsWith)
* [Handleize](#Handleize)
* [Literal](#Literal)
* [LStrip](#LStrip)
* [Pluralize](#Pluralize)
* [Prepend](#Prepend)
* [Remove](#Remove)
* [RemoveFirst](#RemoveFirst)
* [Replace](#Replace)
* [ReplaceFirst](#ReplaceFirst)
* [RStrip](#RStrip)
* [Size](#Size)
* [Slice](#Slice)
* [Slice1](#Slice1)
* [Split](#Split)
* [StartsWith](#StartsWith)
* [Strip](#Strip)
* [StripNewlines](#StripNewlines)
* [ToInt](#ToInt)
* [ToLong](#ToLong)
* [ToFloat](#ToFloat)
* [ToDouble](#ToDouble)
* [Truncate](#Truncate)
* [Truncatewords](#Truncatewords)
* [Upcase](#Upcase)
* [Md5](#Md5)
* [Sha1](#Sha1)
* [Sha256](#Sha256)
* [HmacSha1](#HmacSha1)
* [HmacSha256](#HmacSha256)
* [PadLeft](#PadLeft)
* [PadRight](#PadRight)
* [Base64Encode](#Base64Encode)
* [Base64Decode](#Base64Decode)

TimeSpanFunctions

* [FromDays](#FromDays)
* [FromHours](#FromHours)
* [FromMinutes](#FromMinutes)
* [FromSeconds](#FromSeconds)
* [FromMilliseconds](#FromMilliseconds)
* [Parse](#Parse)


# NTypewriter functions



## ActionFunctions

#### BodyParameter

```csharp
IParameter Action.BodyParameter(IMethod method)
```
Returns parameter that receives content sent to a webapi action in a request body.

#### HttpMethod

```csharp
string Action.HttpMethod(IMethod method)
```
Returns the http method used with a webapi action.            The http method is extracted from Http* or AcceptVerbs attribute or by naming convention if no attributes are specified.

#### Parameters

```csharp
IEnumerable<IParameter> Action.Parameters(IMethod method, bool includeBodyParameter = true)
```
Returns parameters that receive content sent to a webapi action.            If _includeBodyParameter_ is specified as false, then the Parameter list returned will not include the parameter that is being sent in the body of the request.

#### ReturnType

```csharp
IType Action.ReturnType(IMethod method)
```
Returns type that is returned from action unwrapped from Task and ActionResult generics

#### Url

```csharp
string Action.Url(IMethod method)
```
Returns the url for the Web API action based on route attributes (or the supplied convention route if no attributes are present).            Route parameters are converted to TypeScript string interpolation syntax by prefixing all parameters with $ e.g. ${id}.            Optional parameters are added as QueryString parameters for GET and HEAD requests.

----

## ParametersFunctions

#### ToTypeScript

```csharp
IEnumerable<string> Parameters.ToTypeScript(IEnumerable<IParameter> parameters, string nullableType = "null")
```


----

## StringFunctions

#### ToCamelCase

```csharp
string String.ToCamelCase(string text)
```
Converts text case to CamelCase

#### ToUpperFirst

```csharp
string String.ToUpperFirst(string text)
```
Converts first letter of the given string to upper case

#### ToLowerFirst

```csharp
string String.ToLowerFirst(string text)
```
Converts first letter of the given string to lower case

#### SplitIntoSeparateWords

```csharp
IEnumerable<string> String.SplitIntoSeparateWords(string text)
```


----

## SymbolsFunctions

#### WhereNamespaceStartsWith

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNamespaceStartsWith(IEnumerable<ISymbolBase> symbols, string prefix)
```
Filters symbols by the beginning of their namespace

#### WhereNamespaceEndsWith

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNamespaceEndsWith(IEnumerable<ISymbolBase> symbols, string prefix)
```
Filters symbols by the end of their namespace

#### WhereNamespaceMatches

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNamespaceMatches(IEnumerable<ISymbolBase> symbols, string pattern)
```
Filters symbols by regex pattern

#### WhereNameStartsWith

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNameStartsWith(IEnumerable<ISymbolBase> symbols, string prefix)
```
Filters symbols by the beginning of their name

#### WhereNameEndsWith

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNameEndsWith(IEnumerable<ISymbolBase> symbols, string prefix)
```
Filters symbols by the end of their name

#### WhereNameMatches

```csharp
IEnumerable<ISymbolBase> Symbols.WhereNameMatches(IEnumerable<ISymbolBase> symbols, string pattern)
```
Filters symbols by a regex pattern

#### ThatHaveAttribute

```csharp
IEnumerable<ISymbolBase> Symbols.ThatHaveAttribute(IEnumerable<ISymbolBase> symbols, string attributeName)
```
Filters symbols by the presence of an attribute

#### ThatArePublic

```csharp
IEnumerable<ISymbolBase> Symbols.ThatArePublic(IEnumerable<ISymbolBase> symbols)
```
Filters symbols by the public access modifier

#### ThatAreStatic

```csharp
IEnumerable<ISymbolBase> Symbols.ThatAreStatic(IEnumerable<ISymbolBase> symbols)
```
Filters symbols by the static modifier

----

## TypeFunctions

#### AllReferencedTypes

```csharp
IEnumerable<IType> Type.AllReferencedTypes(IType type)
```


#### ToTypeScriptDefault

```csharp
string Type.ToTypeScriptDefault(IType type)
```
The default value of the type.            (Dictionary types returns {}, enumerable types returns [],            boolean types returns false, numeric types returns 0, void returns void(0),            Guid types return empty guid string, Date types return new Date(0),            all other types return null)

#### ToTypeScriptType

```csharp
string Type.ToTypeScriptType(IType type, string nullableTypePostfix = "null")
```
Converts type name to typescript type name

#### Unwrap

```csharp
IType Type.Unwrap(IType type)
```
Returns the first TypeArgument of a generic type or the type itself if it's not generic.

----

## TypesFunctions

#### ThatImplement

```csharp
IEnumerable<IType> Types.ThatImplement(IEnumerable<IType> types, string interfaceName)
```
Filters types based on if a type implements given interface (directly or indirectly)

#### ThatInheritFrom

```csharp
IEnumerable<IType> Types.ThatInheritFrom(IEnumerable<IType> types, string baseTypeName)
```
Filters types based on if a type inherits directly from given type

----

# Scriban functions

Below functions come from Scriban, and you can read more about them in [Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md)



### ArrayFunctions

#### Add

```csharp
IEnumerable Array.Add(IEnumerable list, object value)
```
Adds a value to the input list. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayadd)

#### AddRange

```csharp
IEnumerable Array.AddRange(IEnumerable list1, IEnumerable list2)
```
Concatenates two lists. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayadd_range)

#### Compact

```csharp
IEnumerable Array.Compact(IEnumerable list)
```
Removes any non-null values from the input list. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraycompact)

#### Concat

```csharp
IEnumerable Array.Concat(IEnumerable list1, IEnumerable list2)
```
Concatenates two lists. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayconcat)

#### Cycle

```csharp
object Array.Cycle(TemplateContext context, SourceSpan span, IList list, object group = null)
```
Loops through a group of strings and outputs them in the order that they were passed as parameters. Each time cycle is called, the next string that was passed as a parameter is output. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraycycle)

#### Each

```csharp
ScriptRange Array.Each(TemplateContext context, SourceSpan span, IEnumerable list, object function)
```
Applies the specified function to each element of the input. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayeach)

#### Filter

```csharp
ScriptRange Array.Filter(TemplateContext context, SourceSpan span, IEnumerable list, object function)
```
Filters the input list according the supplied filter function. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayfilter)

#### First

```csharp
object Array.First(IEnumerable list)
```
Returns the first element of the input `list`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayfirst)

#### InsertAt

```csharp
IEnumerable Array.InsertAt(IEnumerable list, int index, object value)
```
Inserts a `value` at the specified index in the input `list`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayinsert_at)

#### Join

```csharp
string Array.Join(TemplateContext context, SourceSpan span, IEnumerable list, string delimiter, object function = null)
```
Joins the element of a list separated by a delimiter string and return the concatenated string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayjoin)

#### Last

```csharp
object Array.Last(IEnumerable list)
```
Returns the last element of the input `list`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraylast)

#### Limit

```csharp
IEnumerable Array.Limit(IEnumerable list, int count)
```
Returns a limited number of elments from the input list 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraylimit)

#### Map

```csharp
IEnumerable Array.Map(TemplateContext context, SourceSpan span, object list, string member)
```
Accepts an array element's attribute as a parameter and creates an array out of each array element's value. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraymap)

#### Offset

```csharp
IEnumerable Array.Offset(IEnumerable list, int index)
```
Returns the remaining of the list after the specified offset 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayoffset)

#### RemoveAt

```csharp
IList Array.RemoveAt(IList list, int index)
```
Removes an element at the specified `index` from the input `list` 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayremove_at)

#### Reverse

```csharp
IEnumerable Array.Reverse(IEnumerable list)
```
Reverses the input `list` 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayreverse)

#### Size

```csharp
int Array.Size(IEnumerable list)
```
Returns the number of elements in the input `list` 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraysize)

#### Sort

```csharp
IEnumerable Array.Sort(TemplateContext context, SourceSpan span, object list, string member = null)
```
Sorts the elements of the input `list` according to the value of each element or the value of the specified `member` of each element 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraysort)

#### Uniq

```csharp
IEnumerable Array.Uniq(IEnumerable list)
```
Returns the unique elements of the input `list`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arrayuniq)

#### Contains

```csharp
bool Array.Contains(IEnumerable list, object item)
```
Returns if an `list` contains an specifique element 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#arraycontains)

----



### HtmlFunctions

#### Strip

```csharp
string Html.Strip(TemplateContext context, string text)
```
Removes any HTML tags from the input string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#htmlstrip)

#### Escape

```csharp
string Html.Escape(string text)
```
Escapes a HTML input string (replacing `&amp;` by `&amp;amp;`) 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#htmlescape)

#### UrlEncode

```csharp
string Html.UrlEncode(string text)
```
Converts any URL-unsafe characters in a string into percent-encoded characters. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#htmlurl_encode)

#### UrlEscape

```csharp
string Html.UrlEscape(string text)
```
Identifies all characters in a string that are not allowed in URLS, and replaces the characters with their escaped variants. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#htmlurl_escape)

----



### MathFunctions

#### Abs

```csharp
object Math.Abs(TemplateContext context, SourceSpan span, object value)
```
Returns the absolute value of a specified number. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathabs)

#### Ceil

```csharp
double Math.Ceil(double value)
```
Returns the smallest integer greater than or equal to the specified number. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathceil)

#### DividedBy

```csharp
object Math.DividedBy(TemplateContext context, SourceSpan span, double value, object divisor)
```
Divides the specified value by another value. If the divisor is an integer, the result will            be floor to and converted back to an integer. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathdivided_by)

#### Floor

```csharp
double Math.Floor(double value)
```
Returns the largest integer less than or equal to the specified number. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathfloor)

#### Format

```csharp
string Math.Format(TemplateContext context, SourceSpan span, object value, string format, string culture = null)
```
Formats a number value with specified [.NET standard numeric format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathformat)

#### IsNumber

```csharp
bool Math.IsNumber(object value)
```
Returns a boolean indicating if the input value is a number 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathis_number)

#### Minus

```csharp
object Math.Minus(TemplateContext context, SourceSpan span, object value, object with)
```
Substracts from the input value the `with` value 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathminus)

#### Modulo

```csharp
object Math.Modulo(TemplateContext context, SourceSpan span, object value, object with)
```
Performs the modulo of the input value with the `with` value 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathmodulo)

#### Plus

```csharp
object Math.Plus(TemplateContext context, SourceSpan span, object value, object with)
```
Performs the addition of the input value with the `with` value 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathplus)

#### Round

```csharp
double Math.Round(double value, int precision = 0)
```
Rounds a value to the nearest integer or to the specified number of fractional digits. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathround)

#### Times

```csharp
object Math.Times(TemplateContext context, SourceSpan span, object value, object with)
```
Performs the multiplication of the input value with the `with` value 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#mathtimes)

----



### RegexFunctions

#### Escape

```csharp
string Regex.Escape(string pattern)
```
Escapes a minimal set of characters (`\`, `*`, `+`, `?`, `|`, `{`, `[`, `(`,`)`, `^`, `$`,`.`, `#`, and white space)            by replacing them with their escape codes.            This instructs the regular expression engine to interpret these characters literally rather than as metacharacters. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexescape)

#### Match

```csharp
ScriptArray Regex.Match(TemplateContext context, string text, string pattern, string options = null)
```
Searches an input string for a substring that matches a regular expression pattern and returns an array with the match occurences. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexmatch)

#### Matches

```csharp
ScriptArray Regex.Matches(TemplateContext context, string text, string pattern, string options = null)
```
Searches an input string for multiple substrings that matches a regular expression pattern and returns an array with the match occurences. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexmatches)

#### Replace

```csharp
string Regex.Replace(TemplateContext context, string text, string pattern, string replace, string options = null)
```
In a specified input string, replaces strings that match a regular expression pattern with a specified replacement string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexreplace)

#### Split

```csharp
ScriptArray Regex.Split(TemplateContext context, string text, string pattern, string options = null)
```
Splits an input string into an array of substrings at the positions defined by a regular expression match. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexsplit)

#### Unescape

```csharp
string Regex.Unescape(string pattern)
```
Converts any escaped characters in the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#regexunescape)

----



### StringFunctions

#### Escape

```csharp
string String.Escape(string text)
```
Escapes a string with escape characters. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringescape)

#### Append

```csharp
string String.Append(string text, string with)
```
Concatenates two strings 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringappend)

#### Capitalize

```csharp
string String.Capitalize(string text)
```
Converts the first character of the passed string to a upper case character. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringcapitalize)

#### Capitalizewords

```csharp
string String.Capitalizewords(string text)
```
Converts the first character of each word in the passed string to a upper case character. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringcapitalizewords)

#### Contains

```csharp
bool String.Contains(string text, string value)
```
Returns a boolean indicating whether the input string contains the specified string `value`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringcontains)

#### Empty

```csharp
bool String.Empty(string text)
```
Returns a boolean indicating whether the input string is an empty string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringempty)

#### Whitespace

```csharp
bool String.Whitespace(string text)
```
Returns a boolean indicating whether the input string is empty or contains only whitespace characters. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringwhitespace)

#### Downcase

```csharp
string String.Downcase(string text)
```
Converts the string to lower case. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringdowncase)

#### EndsWith

```csharp
bool String.EndsWith(string text, string value)
```
Returns a boolean indicating whether the input string ends with the specified string `value`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringends_with)

#### Handleize

```csharp
string String.Handleize(string text)
```
Returns a url handle from the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringhandleize)

#### Literal

```csharp
string String.Literal(string text)
```
Return a string literal enclosed with double quotes of the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringliteral)

#### LStrip

```csharp
string String.LStrip(string text)
```
Removes any whitespace characters on the **left** side of the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringl_strip)

#### Pluralize

```csharp
string String.Pluralize(int number, string singular, string plural)
```
Outputs the singular or plural version of a string based on the value of a number. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringpluralize)

#### Prepend

```csharp
string String.Prepend(string text, string by)
```
Concatenates two strings by placing the `by` string in from of the `text` string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringprepend)

#### Remove

```csharp
string String.Remove(string text, string remove)
```
Removes all occurrences of a substring from a string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringremove)

#### RemoveFirst

```csharp
string String.RemoveFirst(string text, string remove)
```
Removes the first occurrence of a substring from a string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringremove_first)

#### Replace

```csharp
string String.Replace(string text, string match, string replace)
```
Replaces all occurrences of a string with a substring. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringreplace)

#### ReplaceFirst

```csharp
string String.ReplaceFirst(string text, string match, string replace)
```
Replaces the first occurrence of a string with a substring. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringreplace_first)

#### RStrip

```csharp
string String.RStrip(string text)
```
Removes any whitespace characters on the **right** side of the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringr_strip)

#### Size

```csharp
int String.Size(string text)
```
Returns the number of characters from the input string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringsize)

#### Slice

```csharp
string String.Slice(string text, int start, int? length = null)
```
The slice returns a substring, starting at the specified index. An optional second parameter can be passed to specify the length of the substring.            If no second parameter is given, a substring with the remaining characters will be returned. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringslice)

#### Slice1

```csharp
string String.Slice1(string text, int start, int length = 1)
```
The slice returns a substring, starting at the specified index. An optional second parameter can be passed to specify the length of the substring.            If no second parameter is given, a substring with the first character will be returned. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringslice1)

#### Split

```csharp
IEnumerable String.Split(string text, string match)
```
The `split` function takes on a substring as a parameter.            The substring is used as a delimiter to divide a string into an array. You can output different parts of an array using `array` functions. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringsplit)

#### StartsWith

```csharp
bool String.StartsWith(string text, string value)
```
Returns a boolean indicating whether the input string starts with the specified string `value`. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringstarts_with)

#### Strip

```csharp
string String.Strip(string text)
```
Removes any whitespace characters on the **left** and **right** side of the input string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringstrip)

#### StripNewlines

```csharp
string String.StripNewlines(string text)
```
Removes any line breaks/newlines from a string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringstrip_newlines)

#### ToInt

```csharp
object String.ToInt(TemplateContext context, string text)
```
Converts a string to an integer 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringto_int)

#### ToLong

```csharp
object String.ToLong(TemplateContext context, string text)
```
Converts a string to a long 64 bit integer 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringto_long)

#### ToFloat

```csharp
object String.ToFloat(TemplateContext context, string text)
```
Converts a string to a float 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringto_float)

#### ToDouble

```csharp
object String.ToDouble(TemplateContext context, string text)
```
Converts a string to a double 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringto_double)

#### Truncate

```csharp
string String.Truncate(string text, int length, string ellipsis = null)
```
Truncates a string down to the number of characters passed as the first parameter.            An ellipsis (...) is appended to the truncated string and is included in the character count 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringtruncate)

#### Truncatewords

```csharp
string String.Truncatewords(string text, int count, string ellipsis = null)
```
Truncates a string down to the number of words passed as the first parameter.            An ellipsis (...) is appended to the truncated string. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringtruncatewords)

#### Upcase

```csharp
string String.Upcase(string text)
```
Converts the string to uppercase 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringupcase)

#### Md5

```csharp
string String.Md5(string text)
```
Computes the `md5` hash of the input string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringmd5)

#### Sha1

```csharp
string String.Sha1(string text)
```
Computes the `sha1` hash of the input string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringsha1)

#### Sha256

```csharp
string String.Sha256(string text)
```
Computes the `sha256` hash of the input string 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringsha256)

#### HmacSha1

```csharp
string String.HmacSha1(string text, string secretKey)
```
Converts a string into a SHA-1 hash using a hash message authentication code (HMAC). Pass the secret key for the message as a parameter to the function. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringhmac_sha1)

#### HmacSha256

```csharp
string String.HmacSha256(string text, string secretKey)
```
Converts a string into a SHA-256 hash using a hash message authentication code (HMAC). Pass the secret key for the message as a parameter to the function. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringhmac_sha256)

#### PadLeft

```csharp
string String.PadLeft(string text, int width)
```
Pads a string with leading spaces to a specified total length. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringpad_left)

#### PadRight

```csharp
string String.PadRight(string text, int width)
```
Pads a string with trailing spaces to a specified total length. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringpad_right)

#### Base64Encode

```csharp
string String.Base64Encode(string text)
```
Encodes a string to its Base64 representation.            Its character encoded will be UTF-8. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringbase64_encode)

#### Base64Decode

```csharp
string String.Base64Decode(string text)
```
Decodes a Base64-encoded string to a byte array.            The encoding of the bytes is assumed to be UTF-8. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#stringbase64_decode)

----



### TimeSpanFunctions

#### FromDays

```csharp
TimeSpan TimeSpan.FromDays(double days)
```
Returns a timespan object that represents a `days` interval 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanfrom_days)

#### FromHours

```csharp
TimeSpan TimeSpan.FromHours(double hours)
```
Returns a timespan object that represents a `hours` interval 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanfrom_hours)

#### FromMinutes

```csharp
TimeSpan TimeSpan.FromMinutes(double minutes)
```
Returns a timespan object that represents a `minutes` interval 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanfrom_minutes)

#### FromSeconds

```csharp
TimeSpan TimeSpan.FromSeconds(double seconds)
```
Returns a timespan object that represents a `seconds` interval 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanfrom_seconds)

#### FromMilliseconds

```csharp
TimeSpan TimeSpan.FromMilliseconds(double millis)
```
Returns a timespan object that represents a `milliseconds` interval 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanfrom_milliseconds)

#### Parse

```csharp
TimeSpan TimeSpan.Parse(string text)
```
Parses the specified input string into a timespan object. 
[Scriban documentation](https://github.com/scriban/scriban/blob/master/doc/builtins.md#timespanparse)

----



