HashLabel
=========
HashLabel was created to enable click functionality on a label to respond to certain words, such as # and @ tags.

In the following example the red text is created from using a regexp pattern (currently using one supplied by <a href="http://erictarn.com/post/1060722347/the-best-twitter-hashtag-regular-expression" target="_blank">Eric Tarn</a>) and using it in conjunction with NSAttributedString.
The blue rectangles are created by measuring the positions of words in the sentence, then creating and measuring the label, word by word, to find the start and end points of these words.
Word wrapping currently is partially working. It a hashtag is to span three lines, it will not work and subsequently cause the rest of the tags to be out of place.

<img src="https://raw.github.com/ytn3rd/HashLabel/master/Screenshots/iOS%20Simulator%20Screen%20shot%2024.04.2013%208.23.22%20AM.png" />
