import { List } from 'semantic-ui-react';
import SimpleItemAnalysisSegment from './SimpleItemAnalysisSegment';

interface Props {
    simpleItemAnalysisList: any[]
}

export default function SimpleItemAnalysisList({simpleItemAnalysisList}: Props) {
    return (
        <List>
            {simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                <List.Item key={simpleItemAnalysis.id}>
                    <SimpleItemAnalysisSegment 
                        name={simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.name: 'null itemDetails'} 
                        delta={simpleItemAnalysis.delta} 
                        examine={simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.examine: 'null itemDetails'} 
                        high={simpleItemAnalysis.mostRecentSnapshot.high} 
                        highTime={simpleItemAnalysis.mostRecentSnapshot.highTime} 
                        low={simpleItemAnalysis.mostRecentSnapshot.low} 
                        lowTime={simpleItemAnalysis.mostRecentSnapshot.lowTime} 
                        prediction={simpleItemAnalysis.prediction}
                    />
                </List.Item>
            ))}
        </List>
    )
}