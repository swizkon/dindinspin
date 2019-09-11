const requestDinnersType = 'REQUEST_DINNERS';
const receiveDinnersType = 'RECEIVE_DINNERS';
const initialState = { dinners: [], isLoading: false };

export const actionCreators = {
    requestDinners: spinnerId => async (dispatch, getState) => {
        if (spinnerId === getState().dinners.spinnerId) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestDinnersType, spinnerId });

        const url = `api/DinnerSpinner/${spinnerId}/dinners?spinnerId=${spinnerId}`;
        const response = await fetch(url);
        const dinners = await response.json();

        dispatch({ type: receiveDinnersType, spinnerId, dinners });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestDinnersType) {
        return {
            ...state,
            spinnerId: action.spinnerId,
            isLoading: true
        };
    }

    if (action.type === receiveDinnersType) {
        return {
            ...state,
            spinnerId: action.spinnerId,
            dinners: action.dinners,
            isLoading: false
        };
    }

    return state;
};